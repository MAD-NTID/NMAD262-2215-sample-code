from bs4 import BeautifulSoup
import requests

# a constant that represent all went okay with the result
OKAY = 200

# our urls
weather_url = "https://forecast.weather.gov/MapClick.php"
zip_code_url = "https://www.zipinfo.com/cgi-local/zipsrch.exe?ll=ll&zip="

# prompting the user to enter the zipcode
zip_code = input("Enter the zip code:")

# append the desired zipcode to the url
zip_code_url = zip_code_url + zip_code

# make a request to the zip code url to get the lat and lon
response_zip_request = requests.get(zip_code_url)

# check if all is well with the zip request response
if response_zip_request.status_code == OKAY:
    # all is well with the response so we move on to parsing
    zip_html_object = BeautifulSoup(response_zip_request.text, "html5lib")

    # grab all tables, then grab only the 4th table from the array since that is where our result is
    data = zip_html_object.find_all("table")[3]  # parse out all table
    # print(data)
    # we know the lat and lon are stored in the 4th and 5th td so we will parse them out from there and convert them from string to float
    weather_params = {
        "lat": float(data.find_all("td")[3].text),
        "lon": float(data.find_all("td")[4].text)
    }

    # conversion according to the rule in https://gisgeography.com/latitude-longitude-coordinates/
    if data.find_all("th")[3].text == "Latitude(South)":
        weather_params["lat"] *= -1
    if data.find_all("th")[4].text == "Longitude(West)":
        weather_params["lon"] *= -1

else:
    print("Bad request " + zip_code_url)
    exit(0)

# print(weather_params)

# using our newly acquired lat and lon based on the inputed zipcode to make a request to the weather webiste
response_weather_request = requests.get(weather_url, params=weather_params)
if response_weather_request.status_code == OKAY:
    weather_html_object = BeautifulSoup(
        response_weather_request.text, "html5lib")

    # parsing out the location, temperature in both F and C
    location = weather_html_object.find("h2", class_="panel-title")
    tempF = weather_html_object.find(class_="myforecast-current-lrg")
    tempC = weather_html_object.find(class_="myforecast-current-sm")

    print("Current temperature in %s is %s/%s." %
          (location.text, tempF.text, tempC.text))
else:
    print("Bad request " + weather_url)
    exit(0)
