from bs4 import BeautifulSoup
import requests

site_url = "https://forecast.weather.gov/MapClick.php"
params = {
    "lat": "43.1",
    "lon": "-77.5"
}

r = requests.get(site_url, params=params)
if r.status_code == 200:
    soup = BeautifulSoup(r.text, "html5lib")
    temp = soup.find(class_="myforecast-current-lrg")
    print("Current temperature in Rochester, NY is %s." % temp.text)
else:
    print("Bad request")
    exit()
