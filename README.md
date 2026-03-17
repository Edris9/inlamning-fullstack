# inlamning-fulltack



# 🌱 Habit Tracker

En fullstackapplikation för att spåra dagliga vanor.

## Kom igång

1. Klona projektet
2. Gå till `HabitTracker.API`
3. Kör:
```bash
dotnet ef database update
dotnet run
```
4. Öppna `http://localhost:5252`

## Endpoints

| Method | Endpoint | Beskrivning |
|--------|----------|-------------|
| GET | /api/habits | Hämta alla vanor |
| GET | /api/habits/{id} | Hämta en vana |
| POST | /api/habits | Skapa ny vana |
| PUT | /api/habits/{id} | Uppdatera vana |
| DELETE | /api/habits/{id} | Ta bort vana |
| POST | /api/habits/{id}/complete | Markera som klar |

## Teknik

- **Backend:** .NET 10, EF Core, SQLite
- **Frontend:** HTML, CSS, JavaScript (fetch + DOM)

## Reflektion

Gick bra: Strukturen med controllers, services och DTOs kändes tydlig.
Svårt: Få frontend och backend att prata med varandra korrekt.
Förbättring: Kunna lägga till användare och autentisering.