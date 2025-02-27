# SudokuPuzzler  
![Build and test](https://github.com/larsbergqvist/sudoku-puzzler/actions/workflows/dotnet.yml/badge.svg)  

A sudoku puzzle generator in C#

```bash
git clone https://github.com/LarsBergqvist/sudoku-puzzler.git
cd sudoku-puzzler
dotnet run --project Sudoku.Web/Sudoku.Web.csproj --http_ports "5100" --https_ports "5400"
```
Open the Swagger UI in a browser: https://localhost:5400/swagger/index.html 

<p>
You can connect the sudoku-app frontend project with this api, see https://github.com/LarsBergqvist/sudoku-app.git
</p>