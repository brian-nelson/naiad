import naiad.library
import naiad.preprocessor

if __name__ == '__main__':
    credentials = naiad.library.get_credentials()

    if credentials is None:
        exit()

    naiad.library.set_credentials(credentials)
    auth_header = naiad.library.login(credentials)

    if auth_header is not None:
        naiad.library.set_auth_header(auth_header)

        data_type = naiad.library.get_structured_datatype('MastodonPosts')

        posts = naiad.library.get_structured_data(data_type.MetadataId, 0, 1000)

        for post in posts:
            content = post['Content']
            content = naiad.preprocessor.get_text(content)
            content = naiad.preprocessor.remove_urls(content)
            content = naiad.preprocessor.remove_at_tags(content)
            content = naiad.preprocessor.remove_hash_tags(content)
            content = naiad.preprocessor.remove_punctuation(content)
            content = naiad.preprocessor.replace_double_space(content)

            print(content)
            post["content_cleaned"] = content

            tokens = naiad.preprocessor.tokenize_words(content)
            post["no_stopwords"] = naiad.preprocessor.remove_stop_words(tokens)

        print()
