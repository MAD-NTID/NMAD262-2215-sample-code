import requests
import json

geocode_api = {
    "endpoint": "http://api.ipstack.com/check",
    "api_key": "",

}
weather_api = {
    "endpoint": "",
    "api_key": ""
}

client_headers = {
    "User-Agent": "Mozilla/5.0 (Macintosh; Intel Mac OS X 12.1; rv:95.0) Gecko/20100101 Firefox/95.0",
    "Accept": "text/html",
    "Accept-Encoding": "gzip, deflate",
    "Accept-Language": "en-US",
}


def get_content(site_url, params=None, headers=None):
    if headers is None:
        headers = {}
    if params is None:
        params = {}
    try:
        r = requests.get(site_url, params=params, headers=headers)
        r.raise_for_status()

        if r.status_code == 200:
            return r

    except Exception as e:
        print("HTTP ERROR: %s" % e)
        exit()


geocode_params = {
    "access_key": geocode_api["api_key"]
}

r = get_content(geocode_api["endpoint"], params=geocode_params, headers=client_headers)

print(r.content)
