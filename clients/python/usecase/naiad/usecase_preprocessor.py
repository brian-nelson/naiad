import string
import pandas as pd
import re
import nltk
from nltk.corpus import stopwords
from nltk.stem.porter import PorterStemmer
from nltk.stem.snowball import SnowballStemmer
from nltk.corpus import wordnet
from nltk.stem import WordNetLemmatizer
from collections import Counter

# nltk.download()

PUNCTUATION_TO_REMOVE = string.punctuation
# remove stopwords
", ".join(stopwords.words('english'))
STOPWORDS = set(stopwords.words('english'))
wordnet_map = {"N": wordnet.NOUN, "V": wordnet.VERB, "J": wordnet.ADJ, "R": wordnet.ADV}

def remove_punctuation(text):
    """custom function to remove the punctuation"""
    return text.translate(str.maketrans('', '', PUNCTUATION_TO_REMOVE))


def remove_stopwords(text):
    """custom function to remove the stopwords"""
    return " ".join([word for word in str(text).split() if word not in STOPWORDS])


def remove_freqwords(FREQWORDS, text):
    """custom function to remove the frequent words"""
    return " ".join([word for word in str(text).split() if word not in FREQWORDS])


def remove_rarewords(RAREWORDS, text):
    """custom function to remove the rare words"""
    return " ".join([word for word in str(text).split() if word not in RAREWORDS])


def stem_words(stemmer, text):
    return " ".join([stemmer.stem(word) for word in text.split()])


def lemmatize_words(lemmatizer, text):
    return " ".join([lemmatizer.lemmatize(word) for word in text.split()])


def lemmatize_map_words(lemmatizer, text):
    pos_tagged_text = nltk.pos_tag(text.split())
    return " ".join(
        [lemmatizer.lemmatize(word, wordnet_map.get(pos[0], wordnet.NOUN)) for word, pos in pos_tagged_text])


def remove_urls(url_pattern, text):
    text = re.sub(r'(https|http)?:\/\/(\w|\.|\/|\?|\=|\&|\%)*\b', '', text, flags=re.MULTILINE)
    return url_pattern.sub(r'', text)


def cleanhtml(CLEANR, raw_html):
    cleantext = re.sub(CLEANR, '', raw_html)
    return cleantext


def remove_tags(text):
    result = re.sub('<.*?>', '', text)
    return result


def clean_file(file, clean_file):
    pd.options.mode.chained_assignment = None

    full_df = pd.read_csv(file, skipinitialspace=True)
    df = full_df[["hash", "title", "description", "link", "pubDate"]].astype(str)
    print(full_df.head())

    df["description_lower"] = df["description"].str.lower()
    df["title_lower"] = df["title"].str.lower()

    # drop the new columns created in last cell
    df.drop(["title_lower", "description_lower"], axis=1, inplace=True)
    df["description_wo_punct"] = df["description"].apply(lambda x: remove_punctuation(x))
    df["title_wo_punct"] = df["title"].apply(lambda x: remove_punctuation(x))
    df["description_wo_stop"] = df["description_wo_punct"].apply(lambda x: remove_stopwords(x))

    cnt = Counter()
    for text in df["description_wo_stop"].values:
        for word in text.split():
            cnt[word] += 1

    cnt.most_common(10)

    FREQWORDS = set([w for (w, wc) in cnt.most_common(10)])

    df["description_wo_stopfreq"] = df["description_wo_stop"].apply(lambda x: remove_freqwords(FREQWORDS, x))
    df.head()

    # Drop the three columns which are no longer needed
    df.drop(["title_wo_punct", "description_wo_punct", "description_wo_stop"], axis=1, inplace=True)

    n_rare_words = 10
    RAREWORDS = set([w for (w, wc) in cnt.most_common()[:-n_rare_words - 1:-1]])

    df["description_wo_stopfreqrare"] = df["description_wo_stopfreq"].apply(lambda x: remove_rarewords(RAREWORDS, x))
    df.head()

    # Drop the two columns
    df.drop(["description_wo_stopfreq", "description_wo_stopfreqrare"], axis=1, inplace=True)

    # stem words
    stemmer = PorterStemmer()

    df["description_stemmed"] = df["description"].apply(lambda x: stem_words(stemmer, x))
    df.head()

    SnowballStemmer.languages

    # lemmatize words
    lemmatizer = WordNetLemmatizer()

    df["description_lemmatized"] = df["description"].apply(lambda x: lemmatize_words(lemmatizer, x))
    df.head()

    lemmatizer = WordNetLemmatizer()

    df["description_lemmatized"] = df["description"].apply(lambda x: lemmatize_map_words(lemmatizer, x))
    df.head()

    # remove url and html tags
    CLEANR = re.compile('<.*?>|&([a-z0-9]+|#[0-9]{1,6}|#x[0-9a-f]{1,6});')

    df['description_clean'] = df['description'].apply(lambda cw: remove_tags(cw))
    df['title_clean'] = df['title'].apply(lambda cw: remove_tags(cw))
    df['pubDate_clean'] = df['pubDate'].apply(lambda cw: remove_tags(cw))

    text = re.sub(r'^https?:\/\/.*[\r\n]*', '', text, flags=re.MULTILINE)

    # remove ascii and special characters
    df['description_clean'] = df['description_clean'].map(str).apply(
        lambda x: x.encode('utf-8').decode('ascii', 'ignore'))
    df['title_clean'] = df['title_clean'].map(str).apply(lambda x: x.encode('utf-8').decode('ascii', 'ignore'))
    df['pubDate_clean'] = df['pubDate_clean'].map(str).apply(lambda x: x.encode('utf-8').decode('ascii', 'ignore'))

    # Drop the columns which are no longer needed
    df.drop(["description_stemmed", "description_lemmatized", "title", "description", "pubDate"], axis=1, inplace=True)

    df.to_csv(clean_file, index=False, encoding='utf-8')
