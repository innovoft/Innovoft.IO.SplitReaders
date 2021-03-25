# Innovoft.IO.SplitReaders
Innovoft.IO.SplitReaders is a library for efficient csv column reading. When reading small csv files and on one thread using a TextReader and doing a string.Split(...) is not an issue. But when reading 100 GB csv files on 48 threads, string creation ends up being a big deal because of all the GC that happens because of it.
