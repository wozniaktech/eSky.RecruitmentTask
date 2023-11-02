# eSky.RecruitmentTask
Napisz program (aplikacja Web API albo minimal API), który:
1. Pobierze stąd listę autorów: GET https://poetrydb.org/authors
2. Wylosuje z niej 3 losowych autorów.
3. Dla każdego z nich pobierze równolegle listę poematów (GET https://poetrydb.org/author/{author} np. https://poetrydb.org/author/Oscar%20Wilde) i po pobraniu wszystkich zwróci autorów w postaci:
{
  "authors": [
    {
      "name": "Oscar Wilde",
      "poems": [
        "Ballade De Marguerite (Normande)",
        "The Ballad Of Reading Gaol",
        "Easter Day",
        "..."
      ]
    },
    {
      "name": "William Shakespeare",
      "poems": [
        "Spring and Winter i",
        "Spring and Winter ii",
        "Spring",
        "..."
      ]
    },
    {
      "name": "Sir Walter Scott",
      "poems": [
        "Border Ballad",
        "..."
      ]
    }
  ]
}
Należy obsłużyć również sytuacje, gdy nie uda się pobrać poematów dla któregoś z autorów (np. api zwróci status różny od 200). W takiej sytuacji zwracamy kolekcję tylko tych autorów, dla których status odpowiedzi wynosił 200.
Dodać testy jednostkowe.
