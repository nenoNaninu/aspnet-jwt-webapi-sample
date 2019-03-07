# aspnet-jwt-webapi-sample
```
http://localhost:5000/api/token
```
にpostで
```
curl -X POST -H 'Content-Type: application/json' \
  -d '{"username": "mario", "password": "secret"}' \
  0:5000/api/token
```
を投げると
```
{
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJtYXJpbyIsImVtYWlsIjoiaG9nZUBob2dlLmNvbSIsImJpcnRoZGF0ZSI6IjIwMDAtMTItMTIiLCJqdGkiOiI2NGM1Y2ZhYS03ZDZiLTQwYjAtODJkYi0wNDU3ZTgwNzg1ODEiLCJleHAiOjE1NTE3OTY3NTAsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NjM5MzkvIiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo2MzkzOS8ifQ.pHxHEKfK4abmCVu8GfXOLNCPYJS5LH38hnYE93CUrEs"
}
```
みたいのが帰ってくる。
このtokenを$JWTに突っ込んでヘッダにトークンをつけて
```
curl -H 'Authorization: Bearer '$JWT 0:5000/api/books
```
とか投げると認証が通ってリクエストが帰ってくる。

[参考](https://auth0.com/blog/jp-securing-asp-dot-net-core-2-applications-with-jwts/)

