- Open WebLogin solution and build it.

- Inside there is a project called 'WebLogin.Site'. It contains a MVC site with 3 pages that are only accesibles for the user with the correct roles ('PAGE_1', 'PAGE_2', 'PAGE_3'), one for each page. Also there is ADMIN role that can acces to all the pages.

- For autenticate a user, there is a login page.

- There are already created users with the following information:
-- UserName: 'admin', Password: 'admin', Roles: 'ADMIN'
-- UserName: 'user1', Password: 'user1', Roles: 'PAGE_1'
-- UserName: 'user2', Password: 'user2', Roles: 'PAGE_1, PAGE_2'
-- UserName: 'user3', Password: 'user3', Roles: 'PAGE_3'

- The roles is an enum and values are: 
-- NOTHING = 0 (Not used, is the default value)
-- ADMIN = 1
-- PAGE_1 = 2
-- PAGE_2 = 3
-- PAGE_3 = 4

- This project also contain a controller for a web API calls thats exposes a user CRUD operations.

- The REST API use the same credentials that de MVC site but only the user with ADMIN role can crete, update and delete users.

- For call the REST api the headers must contain the key X-ApiKey with the value 'MyRandomApiKeyValue' and use a basic http authoritzation with the username and password.

- There is also a project called WebLogin.SiteTests that contains MSTest of REST API.