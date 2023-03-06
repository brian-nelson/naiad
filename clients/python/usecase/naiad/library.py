# This is a sample Python script.
import os
import requests
import urllib3
import shutil

# For Development ignore insecure request warnings.
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)
verifySSL = False

# Define module level variables to simplify
credentials = None
auth_header = None


# Credential object that stores url, user and password for Naiad
class Credentials(object):
    def __init__(self, url, user, password):
        self.Url = url
        self.User = user
        self.Password = password


# Object used to track structured data type
class StructuredDataDefinition(object):
    def __init__(self, metadata_id, name, description, mimetype, identifier_name):
        self.MetadataId = metadata_id
        self.Name = name
        self.Description = description
        self.MimeType = mimetype
        self.IdentifierName = identifier_name


# Set credentials
def set_credentials(my_credentials):
    global credentials
    credentials = my_credentials


# Set auth headers
def set_auth_header(my_auth_header):
    global auth_header
    auth_header = my_auth_header


# Retrieves the credentials from environment variables
def get_credentials():
    url = os.environ["TEST_NAIAD_URL"]
    user = os.environ["TEST_NAIAD_USER"]
    password = os.environ["TEST_NAIAD_PASSWORD"]

    return Credentials(url, user, password)


# Logs into Naiad and returns the Auth header needed for subsequent calls
def login(credentials_param):
    url = credentials_param.Url + 'login'
    body = {'Email': credentials_param.User, 'Password': credentials_param.Password}

    response = requests.post(url, json=body, verify=verifySSL)

    if response.status_code == requests.codes.ok:
        response_json = response.json()
        jwt = response_json['JWT']

        header = {'AUTHORIZATION': 'Bearer ' + jwt}
        return header

    return None


# Lists the files that have been loaded to Naiad
def list_files():
    url = credentials.Url + 'data/list'

    response = requests.get(url, headers=auth_header, verify=verifySSL)

    if response.status_code == requests.codes.ok:
        response_json = response.json()
        return response_json

    return None


# Retrieves a file and writes it to the local file system
def get_file(remote_path, local_path):
    url = credentials.Url + 'data/' + remote_path

    with requests.get(url, headers=auth_header, verify=verifySSL, stream=True) as r:
        with open(local_path, 'wb') as f:
            shutil.copyfileobj(r.raw, f)

    return local_path


# Gets the file pointer for a file from Naiad
def get_file_pointer(remote_path):
    url = credentials.Url + 'data/pointer/' + remote_path

    response = requests.get(url, headers=auth_header, verify=verifySSL, stream=True)

    if response.status_code == requests.codes.ok:
        response_json = response.json()
        return response_json['DataPointerId']

    return None


# Loads a local file to Naiad
def send_file(local_path, remote_path):
    url = credentials.Url + 'data/' + remote_path

    with open(local_path, 'rb') as f:
        response = requests.post(url, headers=auth_header, verify=verifySSL, files={'file': f})

        if response.status_code == requests.codes.ok:
            response_json = response.json()
            return response_json['DataPointerId']

        return None


# Retrieves a Structure Data Definition
def get_structured_datatype(datatype):
    url = credentials.Url + 'definition/' + datatype

    response = requests.get(url, headers=auth_header, verify=verifySSL)

    if response.status_code == requests.codes.ok:
        response_json = response.json()

        obj = StructuredDataDefinition(response_json['MetadataId'],
                                       response_json['Name'],
                                       response_json['Description'],
                                       response_json['MimeType'],
                                       response_json['IdentifierName'])
        return obj


# Transforms a Naiad file into structured data
def transform_file(data_pointer_id, metadata_id):
    url = credentials.Url + 'data/' + data_pointer_id + '/transform/' + metadata_id

    response = requests.post(url, headers=auth_header, verify=verifySSL)

    if response.status_code == requests.codes.ok:
        return True

    return False


# Retrieves a set of structured data.  Skip and Limit allow a small subset to be retrieved
def get_structured_data(metadata_id, skip, limit):
    url = credentials.Url + 'structured/' + metadata_id + '/' + str(skip) + '/' + str(limit)

    response = requests.get(url, headers=auth_header, verify=verifySSL)

    if response.status_code == requests.codes.ok:
        return response.json()

