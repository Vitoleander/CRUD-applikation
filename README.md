### CRUD-applikation - Angular 20 & .NET 9
> Inlämningsuppgift – Responsiv CRUD-applikation med tokenhantering

Detta projekt är en fullstack-applikation byggd med:
* Angular 20 (standalone components, Angular signals, Bootstraps, Fontawsome)
* .Net 9 WebAPI med JWT-autentisering
* Mock-databutik (ingen extern DB, allt lagras i minnet)
* CORS-konfigurering för webbaserad utvecklingsmiljö (Github Codespaces)
Syftet är att skapa en modern responsiv CRUD-applikation där användaren kan registrera sig, logga in,
hantera böcker och citat.

>Funktioner som är implementerade

🔐 Autentisering (JWT)
* Registrering av ny användare
* Inloggning som returnerar JWT-token
* Token lagras i localStorage
* Token skickas automatiskt i alla API-requests via interceptor
* Endast inloggade användare kan:
* Se sina böcker
* Skapa/redigera/radera böcker
* Se, lägga till, redigera och ta bort sina citat

📖 Bokhantering (Books CRUD)
* Backend
* Böcker lagras i en mockad in-memory store (BookStore)
* Endast böcker som tillhör inloggad användare returneras
* Endpoints:
  * GET /api/Books (kräver token)
  * POST /api/Books
  * PUT /api/Books/{id}
  * DELETE /api/Books/{id}
* Frontend
* Lista alla böcker
* Formulär för att skapa ny bok
* Formulär för att redigera bok
* Radera bok med bekräftelse
* Snygg responsiv tabell med Bootstrap

  💬 Citatfunktion ("Mina citat")
* Backend
* Citat lagras i QuoteStore
* Citat knyts till rätt användare automatiskt via token (JWT claim)
* Frontend
* Lista användarens citat
* Visa datumformat med Angular pipes
* Radera citat
* Knapp: “Lägg till nytt citat”
* Redigeringsvy (påbörjad)

🎨 UI / UX
* Bootstrap används för layout
* FontAwesome för ikoner
* Navigationsmeny som uppdateras korrekt vid login/logout
* Mobilvänlig hamburgarmeny
* Standalone Angular components
* Responsiva listor och formulär

###🏗️ Teknisk översikt
Backend (BookApi) – .NET 9
* JWT-autentisering
* Controllers:
 * UserController
 * BooksController
 * QuoteController
* Mock-databutik:
 * UserStore
 * BookStore
 * QuoteStore
* CORS aktiverat för Angular

Frontend (Angular 20)
* Standalone components
* Routing
* Services (auth, books, quotes)
* Interceptor som lägger till Authorization: Bearer <token>
* Views:
  * Login
  * Register
  * Book list
  * Book form
  * Quotes list


✔ Status:

Backend fungerar fullt ut
Autentisering fungerade innan gratisverion av github tog slut
Boklista + CRUD fungerar

### Installation

OS X & Linux:
Backend:
```sh
cd backend/BookApi
dotnet build
dotnet run
```
Frontend
```sh
cd frontend/frontend-app
npm install
npm start
```

🌐 Deployment (Frontend)

Projektet kan enkelt deployas via Netlify, Vercel eller liknande.

Netlify:

Build command: npm run build
Publish directory: dist/frontend-app/browser

