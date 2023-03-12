# import libraries
import requests
from bs4 import BeautifulSoup
import pandas as pd

# enter URLs
urls = ["https://feeds.feedburner.com/TheHackersNews?format=xml",
        "https://www.csoonline.com/in/index.rss",
        "https://www.darkreading.com/rss.xml",
        "https://threatpost.com/feed/",
        "https://nakedsecurity.sophos.com/feed/",
        "https://www.infosecurity-magazine.com/rss/news/",
        "https://medium.com/feed/@2ndsightlab",
        "https://www.theguardian.com/technology/data-computer-security/rss",
        "https://www.mcafee.com/blogs/feed/",
        "http://feeds.trendmicro.com/TrendMicroResearch",
        "http://www.techrepublic.com/rssfeeds/topic/security/?feedType=rssfeeds",
        "https://www.computerworld.com/uk/category/security/index.rss",
        "https://itsecuritycentral.teramind.co/feed/",
        "https://latesthackingnews.com/feed/",
        "http://blog.isc2.org/isc2_blog/atom.xml",
        "https://feeds.feedburner.com/govtech/blogs/lohrmann_on_infrastructure",
        "https://hakin9.org/feed/",
        "https://davinciforensics.co.za/cybersecurity/feed/",
        "https://cybersecurity.att.com/site/blog-all-rss",
        "https://www.officialhacker.com/feed/",
        "http://thehackernews.com/feeds/posts/default",
        "http://rss.acast.com/cyber",
        "https://freedomhacker.net/feed/",
        "https://cybersecurityinside.libsyn.com/rss",
        "https://australiancybersecuritymagazine.com.au/feed/",
        "https://blog.knowbe4.com/rss.xml",
        "http://cybersecuritytoday.libsyn.com/rss",
        "https://cybersecuritysauna.libsyn.com/rss",
        "https://gbhackers.com/feed/",
        "https://www.hackread.com/feed/",
        "http://nakedsecurity.sophos.com/feed/",
        "https://www.helpnetsecurity.com/feed/",
        "https://www.troyhunt.com/rss/",
        "https://www.computerworld.com/index.rss",
        "https://www.tripwire.com/state-of-security/feed/",
        "https://www.welivesecurity.com/feed/",
        "http://feeds.feedburner.com/Securityweek",
        "https://www.webroot.com/blog/feed/",
        "https://insights.sei.cmu.edu/blog/feeds/latest/rss/",
        "https://securityintelligence.com/category/x-force/feed/",
        "https://www.janes.com/feeds/news",
        "http://www.veracode.com/blog/feed/",
        "http://blog.webroot.com/feed",
        "http://securityaffairs.co/wordpress/feed",
        "https://www.upguard.com/blog/rss.xml",
        "https://www.upguard.com/breaches/rss.xml",
        "https://heimdalsecurity.com/blog/feed/",
        "https://www.flyingpenguin.com/?feed=rss2",
        "https://www.lastwatchdog.com/feed/",
        "https://blogs.quickheal.com/feed/",
        "https://www.netsparker.com/blog/rss/",
        "https://taosecurity.blogspot.com/feeds/posts/default?alt=rss",
        "https://any.run/cybersecurity-blog/feed/",
        "https://www.exploitone.com/feed/",
        "https://www.nist.gov/blogs/cybersecurity-insights/rss.xml",
        "https://www.acunetix.com/blog/feed/",
        "https://blog.pcisecuritystandards.org/rss.xml",
        "https://tyksinski.com/rss/",
        "https://media.zencast.fm/tevora-talks-info-sec-podcast/rss",
        "https://www.erdalozkaya.com/feed/",
        "https://thecyberexpress.com/feed",
        "https://socprime.com/feed/",
        "https://www.secpod.com/blog/feed/",
        "https://binaryblogger.com/feed/",
        "https://www.cm-alliance.com/cybersecurity-blog/rss.xml",
        "https://www.securitymagazine.com/rss/articles",
        "https://www.explo-media.com/blog/feed/",
        "https://secureblitz.com/feed/",
        "https://techtalk.pcmatic.com/feed/",
        "https://www.cheapsslshop.com/blog/feed/",
        "https://www.binarydefense.com/feed/",
        "https://www.reveantivirus.com/blog/lan/en/feed",
        "https://blog.securityinnovation.com/rss.xml",
        "https://cybersguards.com/feed/",
        "https://feed.podbean.com/cscp/feed.xml",
        "https://blog.gitguardian.com/rss/",
        "https://www.blackfog.com/feed/",
        "https://marcoramilli.com/feed/",
        "https://lab.wallarm.com/feed",
        "https://www.cybercrimeswatch.com/feed/",
        "https://sectigostore.com/blog/feed/",
        "https://defenseindepth.libsyn.com/rss",
        "https://wesecureapp.com/feed/",
        "https://www.cybersecuritycloudexpo.com/feed/",
        "https://www.secureblink.com/rss-feeds/threat-feed",
        "https://tacsecurity.com/feed/",
        "https://cyberhoot.com/category/blog/feed/",
        "https://privacysavvy.com/feed/",
        "https://blog.mazebolt.com/rss.xml",
        "https://www.vistainfosec.com/feed/",
        "https://dataprivacymanager.net/feed/",
        "https://blog.g5cybersecurity.com/feed/",
        "https://www.tsfactory.com/forums/blogs/category/infosec-digest/feed/",
        "https://blog.entersoftsecurity.com/feed/",
        "https://be4sec.com/feed/",
        "https://www.clearnetwork.com/feed/",
        "https://www.cyberpilot.io/cyberpilot-blog/rss.xml",
        "https://cnsight.io/blog/feed/",
        "https://cyberworkx.in/feed/",
        "https://securityparrot.com/feed/",
        "https://www.anomali.com/site/blog-rss",
        "https://shawnetuma.com/feed/",
        "https://diycyber.libsyn.com/rss",
        "http://www.securitynewspaper.com/feed",
        "https://www.brighttalk.com/channel/288/feed/rss",
        "https://steptoecyber.libsyn.com/rss",
        "https://www.tisiphone.net/feed/",
        "https://www.hackercombat.com/feed",
        "https://ransomware.databreachtoday.com/rss-feeds",
        "https://krebsonsecurity.com/category/ransomware/feed/",
        "https://hackercombat.com/feed/",
        "https://anchor.fm/s/53b28d44/podcast/rss",
        "http://www.thesolutionfirm.com/blog/?feed=rss2",
        "https://anchor.fm/s/1eff9ad8/podcast/rss",
        "https://krebsonsecurity.com/feed",
        "https://feeds.feedburner.com/ComputerCrimeResearchNews",
        "https://anchor.fm/s/14d32f34/podcast/rss",
        "http://feeds.feedburner.com/eset/blog",
        "https://brilliancesecuritymagazine.com/feed/",
        "https://www.cyberdefensemagazine.com/feed/",
        "https://www.cisomag.com/feed/",
        "https://www.uscybersecurity.net/feed/",
        "https://cybersecurity-magazine.com/feed/",
        "https://cyberprotection-magazine.com/feed/",
        "https://www.securitymagazine.com/rss/topic/2788",
        "https://cybersecurity.springeropen.com/articles/most-recent/rss.xml",
        "https://threat.technology/feed/"
        ]
all_items = []
for url in urls:

    resp = requests.get(url)

    soup = BeautifulSoup(resp.content, features="xml")

    print(soup.prettify())

    items = soup.findAll('item')
    print(items)

    print(url)

    item = items[1]
    item

    item.title

    item.title.text

    # declare empty var to append data
    news_items = []

    # scarring HTML tags such as Title, Description, Links
    for item in items:
        news_item = {}
        news_item['title'] = item.title.text
        news_item['description'] = item.description.text
        news_item['link'] = item.link.text
        news_item['pubDate'] = item.pubDate
        news_items.append(news_item)

    print(news_items)
    all_items.extend(news_items)

    news_items[0]

# create dataframe of the data
df = pd.DataFrame(all_items, columns=['title', 'description', 'link', 'pubDate'])

df.head()

# convert df to CSV
df.to_csv('RSSdata.csv', index=False, encoding='utf-8')
