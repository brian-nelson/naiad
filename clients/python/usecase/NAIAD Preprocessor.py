import numpy as np
import pandas as pd
import re
import nltk
import spacy
import string
from nltk.corpus import stopwords
from nltk.stem.porter import PorterStemmer
from nltk.stem.snowball import SnowballStemmer
from nltk.corpus import wordnet
from nltk.stem import WordNetLemmatizer
from collections import Counter
pd.options.mode.chained_assignment = None

full_df = pd.read_csv("C:/Users/Owner/Desktop/NU Capstone/NAIAD/RSSdata.csv", nrows=5111, skipinitialspace=True)
df = full_df[["title", "description", "link", "pubDate"]].astype(str)
print(full_df.head())

df["description_lower"] = df["description"].str.lower()
df["title_lower"] = df["title"].str.lower()
df.head()

# drop the new columns created in last cell
df.drop(["title_lower", "description_lower"], axis=1, inplace=True)

PUNCT_TO_REMOVE = string.punctuation
def remove_punctuation(text):
    """custom function to remove the punctuation"""
    return text.translate(str.maketrans('', '', PUNCT_TO_REMOVE))

df["description_wo_punct"] = df["description"].apply(lambda text: remove_punctuation(text))
df["title_wo_punct"] = df["title"].apply(lambda text: remove_punctuation(text))
df.head()

# remove stopwords
", ".join(stopwords.words('english'))

STOPWORDS = set(stopwords.words('english'))
def remove_stopwords(text):
    """custom function to remove the stopwords"""
    return " ".join([word for word in str(text).split() if word not in STOPWORDS])

df["description_wo_stop"] = df["description_wo_punct"].apply(lambda text: remove_stopwords(text))
df.head()

cnt = Counter()
for text in df["description_wo_stop"].values:
    for word in text.split():
        cnt[word] += 1

cnt.most_common(10)

FREQWORDS = set([w for (w, wc) in cnt.most_common(10)])
def remove_freqwords(text):
    """custom function to remove the frequent words"""
    return " ".join([word for word in str(text).split() if word not in FREQWORDS])

df["description_wo_stopfreq"] = df["description_wo_stop"].apply(lambda text: remove_freqwords(text))
df.head()

# Drop the three columns which are no longer needed
df.drop(["title_wo_punct", "description_wo_punct", "description_wo_stop"], axis=1, inplace=True)

n_rare_words = 10
RAREWORDS = set([w for (w, wc) in cnt.most_common()[:-n_rare_words-1:-1]])
def remove_rarewords(text):
    """custom function to remove the rare words"""
    return " ".join([word for word in str(text).split() if word not in RAREWORDS])

df["description_wo_stopfreqrare"] = df["description_wo_stopfreq"].apply(lambda text: remove_rarewords(text))
df.head()

# Drop the two columns
df.drop(["description_wo_stopfreq", "description_wo_stopfreqrare"], axis=1, inplace=True)

# stem words
stemmer = PorterStemmer()
def stem_words(text):
    return " ".join([stemmer.stem(word) for word in text.split()])

df["description_stemmed"] = df["description"].apply(lambda text: stem_words(text))
df.head()

SnowballStemmer.languages

# lemmatize words
lemmatizer = WordNetLemmatizer()
def lemmatize_words(text):
    return " ".join([lemmatizer.lemmatize(word) for word in text.split()])

df["description_lemmatized"] = df["description"].apply(lambda text: lemmatize_words(text))
df.head()

lemmatizer = WordNetLemmatizer()
wordnet_map = {"N":wordnet.NOUN, "V":wordnet.VERB, "J":wordnet.ADJ, "R":wordnet.ADV}
def lemmatize_words(text):
    pos_tagged_text = nltk.pos_tag(text.split())
    return " ".join([lemmatizer.lemmatize(word, wordnet_map.get(pos[0], wordnet.NOUN)) for word, pos in pos_tagged_text])

df["description_lemmatized"] = df["description"].apply(lambda text: lemmatize_words(text))
df.head()

# remove url and html tags
def remove_urls(text):
    text = re.sub(r'(https|http)?:\/\/(\w|\.|\/|\?|\=|\&|\%)*\b', '', text, flags=re.MULTILINE)
    return url_pattern.sub(r'', text)

CLEANR = re.compile('<.*?>|&([a-z0-9]+|#[0-9]{1,6}|#x[0-9a-f]{1,6});')

def cleanhtml(raw_html):
  cleantext = re.sub(CLEANR, '', raw_html)
  return cleantext

def remove_tags(text):
    result = re.sub('<.*?>','', text)
    return result
df['description_clean']=df['description'].apply(lambda cw : remove_tags(cw))
df['title_clean']=df['title'].apply(lambda cw : remove_tags(cw))
df['pubDate_clean']=df['pubDate'].apply(lambda cw : remove_tags(cw))

text = re.sub(r'^https?:\/\/.*[\r\n]*', '', text, flags=re.MULTILINE)

# remove ascii and special characters
df['description_clean'] = df['description_clean'].map(str).apply(lambda x: x.encode('utf-8').decode('ascii', 'ignore'))
df['title_clean'] = df['title_clean'].map(str).apply(lambda x: x.encode('utf-8').decode('ascii', 'ignore'))
df['pubDate_clean'] = df['pubDate_clean'].map(str).apply(lambda x: x.encode('utf-8').decode('ascii', 'ignore'))

# Drop the columns which are no longer needed
df.drop(["description_stemmed", "description_lemmatized", "title", "description", "pubDate"], axis=1, inplace=True)

df.to_csv('RSSData_clean.csv', index=False, encoding='utf-8')
