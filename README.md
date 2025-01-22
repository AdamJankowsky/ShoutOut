# ShoutOut
ShoutOut is social blog platform similar to Wykop's Mikroblog

I am creating it as hobby project because I wanted to know how much time would it consume to create such site.

I want to use PostgreSql to store data, ASP.NET Core 8 as backend, Blazor with Server Side Rendering as frontend.

For Authentication I want to implement User/Password authentication with storing Users in db with Passwords hashed with PBKDF2
For asynchronous changes RabbitMq will be used with MassTransit.

For architecture perspective that would be all.

Basic features to implement:

1. Creating account.
2. Managing account - Change password, change avatar, change nickname 
3. Adding posts
4. Liking posts
5. Adding comments to posts
6. Liking comments
7. Showing only best posts in 2, 4, 6 hours
8. Adding tags and searching by them
9. Showing user's profile
10. Showing single post page
11. Adding user to blocked lists - which results in not showing user's posts on wall and hiding comments
12. That would all for now.
