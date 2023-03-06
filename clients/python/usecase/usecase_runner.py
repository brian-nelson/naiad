import naiad.library
import naiad.usecase_data
import naiad.usecase_preprocessor
import requests
from bs4 import BeautifulSoup
import pandas as pd
from dateutil import parser
import hashlib

filename = 'c:\\temp\\RSSdata.csv'
filename_clean = 'c:\\temp\\RSSdata_clean.csv'

if __name__ == '__main__':
    credentials = naiad.library.get_credentials()

    if credentials is None:
        exit()

    naiad.library.set_credentials(credentials)
    auth_header = naiad.library.login(credentials)

    if auth_header is not None:
        naiad.library.set_auth_header(auth_header)

        # urls = naiad.usecase_data.cyber_security_urls
        #
        # all_items = []
        # for url in urls:
        #     try:
        #         resp = requests.get(url)
        #         soup = BeautifulSoup(resp.content, features="xml")
        #
        #         items = soup.findAll('item')
        #
        #         item = items[1]
        #
        #         # declare empty var to append data
        #         news_items = []
        #
        #         # scarring HTML tags such as Title, Description, Links
        #         for item in items:
        #             title = item.title.text
        #             description = item.description.text
        #             link = item.link.text
        #             pub_date = str(parser.parse(item.pubDate.text))
        #
        #             hasher = hashlib.new('sha256')
        #             hasher.update(title.encode())
        #             hasher.update(description.encode())
        #
        #             hash_value = str(int(hasher.hexdigest(), 16))
        #
        #             news_item = {
        #                 'hash': hash_value,
        #                 'title': title,
        #                 'description': description,
        #                 'link': link,
        #                 'pubDate': pub_date
        #             }
        #
        #             news_items.append(news_item)
        #
        #         all_items.extend(news_items)
        #     except:
        #         print('An error occurred on - ' + url)
        #
        # # create dataframe of the data
        # df = pd.DataFrame(all_items, columns=['hash', 'title', 'description', 'link', 'pubDate'])
        # df.head()
        # df.to_csv(filename, index=False, encoding='utf-8')
        #
        # file_pointer = naiad.library.send_file(filename, 'RSSdata.csv')

        # clean the file

        naiad.usecase_preprocessor.clean_file(filename, filename_clean)
