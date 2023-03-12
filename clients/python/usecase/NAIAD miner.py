import matplotlib.pyplot as plt
import nltk
import pandas as pd
from nltk.tokenize import RegexpTokenizer
from nltk.tokenize import word_tokenize
from nltk.corpus import stopwords
from nltk.probability import FreqDist
from nltk.stem import WordNetLemmatizer
from nltk.sentiment import SentimentIntensityAnalyzer
import seaborn as sns
import plotly.express as px
import plotly.io as pio
import matplotlib.pyplot as plt

df = pd.read_csv('RSSData_clean.csv', engine='python')

df['description_clean'] = df['description_clean'].astype(str).str.lower()

regexp = RegexpTokenizer('\w+')

df['desc_token'] = df['description_clean'].apply(regexp.tokenize)

stopwords = nltk.corpus.stopwords.words("english")

my_stopwords = ['https', 'thi', 'com', 'www', 'help', 'nbsp', 'podcast', 'blog', 'new', 'artcle', 'linkedin', 'get',
                'one', 'first', 'episode', 'book', 'video', 'anchor', 'breakingintocybersecurity', 'helping', 'use',
                'breaking', 'best', 'year', 'using', 'years', 'message', 'share', 'support', 'hiring', 'blogger']
stopwords.extend(my_stopwords)

df['desc_token'] = df['desc_token'].apply(lambda x: [item for item in x if item not in stopwords])

df['desc_string'] = df['desc_token'].apply(lambda x: ' '.join([item for item in x if len(item) > 2]))

all_words = ' '.join([word for word in df['desc_string']])

tokenized_words = nltk.tokenize.word_tokenize(all_words)

fdist = FreqDist(tokenized_words)
fdist

df['desc_string_fdist'] = df['desc_token'].apply(lambda x: ' '.join([item for item in x if fdist[item] >= 3 ]))

wordnet_lem = WordNetLemmatizer()
df['desc_string_lem'] = df['desc_string_fdist'].apply(wordnet_lem.lemmatize)

all_words_lem = ' '.join([word for word in df['desc_string_lem']])

words = nltk.word_tokenize(all_words_lem)
fd = FreqDist(words)

print(fd.most_common(5))

analyzer = SentimentIntensityAnalyzer()

df['polarity'] = df['desc_string_lem'].apply(lambda x: analyzer.polarity_scores(x))

# Change data structure
df = pd.concat(
    [df.drop(['polarity'], axis=1),
     df['polarity'].apply(pd.Series)], axis=1)

# Create new variable with sentiment "neutral," "positive" and "negative"
df['sentiment'] = df['compound'].apply(lambda x: 'positive' if x >0 else 'neutral' if x==0 else 'negative')

print(df.loc[df['compound'].idxmax()].values)

print(df.loc[df['compound'].idxmin()].values)

df.to_csv('RSSData_clean_final.csv', index=False, encoding='utf-8')

sns.countplot(y='sentiment',
             data=df,
             palette=['#b2d8d8',"#008080", '#db3d13']
             )
plt.show()
