
WebCrawler
README.md

To run WebCrawler using Kestrel locally:
use cmd: dotnet run

Navigate here:
http://localhost:5000/api/crawler/crawlasynchronously
https://localhost:5001/api/crawler/crawlasynchronously

Running from Docker locally:

docker build -t wcc .
docker run -p 3000:80 wcc
