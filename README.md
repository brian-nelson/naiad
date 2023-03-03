# Naiad Data Lake
Naiad is designed to be a scalable networked data lake.  It was intially constructed as part of a Masters Thesis project.

# Design Objectives

1. Create a data lake with modular data storage options.  The modularity should enable the data lake to grow from a raspberry pi computer, up to a cluster.
2. Create a data lake that has built in data sharing.  The data sharing should be modular and swappable.  Connectors objects will implement wrap any networking implementations.  It is desired to use existing protocols like ActivityPub.


# Data Storage Providers

1. Initially LiteDb is being used as the data repository for Naiad.  See https://github.com/mbdavid/LiteDB for more information


# Connectors

1. The ActivityPub provider will implement the minimum needed to interact (pub/sub) with other servers.


# Acknowlegements

1. The ActivityPub implementation is adapted from Fedodo's Fedodo Server.  See https://github.com/Fedodo/Fedodo.Server for more information.
