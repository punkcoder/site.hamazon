# Security Issues

The goal of this application is to show off security issues that have been witnessed in the public space.

membership user: testuser@gmail.com:P@ssw0rd

## SQL Injection
1. SQL Map via: python sqlmap.py -u "http://hamazon.localpc.com/products/product-detail-page/?sku=00001" --level=5 --risk=2 --dump-all --threads 10 -o --time-sec=1
2. 

## Authentication / Session Management
1. Dont store sensitive values in JWT Tokens... (Check Here)[https://jwt.io/]
2. Dont use the default password from websites... (Click Here)[https://github.com/jwt-dotnet/jwt]

## Cross Site Scripting
1. Cross Site Scripting is available through the comment module on one of the product detail page.

## Insecure Direct Object References
1. The query string that is in the member profile can be iterated on the integer to get direct access to user infromation. 