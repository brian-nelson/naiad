import re
import string
import nltk
from nltk.tokenize import word_tokenize
from bs4 import BeautifulSoup

# Setup stop words
stopwords = nltk.corpus.stopwords.words("english")
my_stopwords = ['https', 'thi', 'com', 'www', 'help', 'nbsp', 'podcast', 'blog', 'new', 'artcle', 'linkedin', 'get',
                'one', 'first', 'episode', 'book', 'video', 'anchor', 'breakingintocybersecurity', 'helping', 'use',
                'breaking', 'best', 'year', 'using', 'years', 'message', 'share', 'support', 'hiring', 'blogger']
stopwords.extend(my_stopwords)

def get_text(text):
    soup = BeautifulSoup(text, features="lxml")
    output = soup.get_text()
    return output


def remove_urls(text):
    # output = re.sub(r'(https?:\/\/)(\s)*(www\.)?(\s)*((\w|\s)+\.)*([\w\-\s]+\/)*([\w\-]+)((\?)?[\w\s]*=\s*[\w\%&]*)*', ' ', text, flags=re.MULTILINE)
    output = re.sub(r'http\S+', ' ', text, flags=re.MULTILINE)
    # output = re.sub(r'(https|http)?:\/\/(\w|\.|\/|\?|\=|\&|\%)*\b', '', text, flags=re.MULTILINE)
    return output


def remove_at_tags(text):
    output = re.sub(r'@\w+', ' ', text, flags=re.MULTILINE)
    return output


def remove_hash_tags(text):
    output = re.sub(r'#\w+', ' ', text, flags=re.MULTILINE)
    return output


def remove_punctuation(text):
    output = text

    for character in string.punctuation:
        if character != '’' and character != '\'':
            output = output.replace(character, ' ')

    return output


def replace_double_space(text):
    output = text

    output = output.replace('  ', ' ')

    return output


def tokenize_words(text):
    output = word_tokenize(text.strip())
    return output


def remove_stop_words(tokens):
    output = []

    for item in tokens:
        if item not in stopwords:
            output.append(item)

    return output
