import naiad.library

if __name__ == '__main__':
    credentials = naiad.library.get_credentials()

    if credentials is None:
        exit()

    naiad.library.set_credentials(credentials)
    auth_header = naiad.library.login(credentials)

    if auth_header is not None:
        naiad.library.set_auth_header(auth_header)
        # example of sending file to Naiad
        # file_pointer = naiad.library.send_file('c:\\temp\\mitre_allitems_2018.csv', 'testdata/mitre_allitems_2018.csv')

        # example of getting a file pointer
        file_pointer = naiad.library.get_file_pointer('testdata/mitre_allitems_2018.csv')
        print('File Pointer: ' + file_pointer)

        if file_pointer is not None:
            # Example of pulling down file from Naiad
            # naiad.library.get_file('testdata/mitre_allitems_2018.csv', 'c:\\temp\\mitre_allitems_2018-2.csv')

            # Example of getting Structured Data Type
            sdd = naiad.library.get_structured_datatype('CVE')
            print('SDD Id: ' + sdd.MetadataId)

            # Example of transforming file to Structured Data in Naiad.
            # transform_result = transform_file(file_pointer, sdd.MetadataId)

            # if transform_result:
            #     print('Transform successful')
            # else:
            #     print('Transform failed')

            # Get structured data - First 100 rows
            structured_data = naiad.library.get_structured_data(sdd.MetadataId, 0, 100)

            # Print first rows name and description
            print(structured_data[0]['Name'])
            print(structured_data[0]['Description'])

            # Get the second hundred rows
            structured_data = naiad.library.get_structured_data(sdd.MetadataId, 100, 100)

            # Print the 101 row's description
            print(structured_data[0]['Name'])
            print(structured_data[0]['Description'])
