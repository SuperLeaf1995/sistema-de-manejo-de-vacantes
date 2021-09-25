# Sistema de ASP.NET para el manejo de Vacantes
Sistema utilizando tecnologies C#, ASP.NET y Microsoft SQL Server, con Python Unit testing.

# Poblar base de datos
El Schema.sql sirve como una secuencia de comandos SQL para poblar las tablas y estructuras iniciales:

```sh
sqlcmd -S localhost -U SA -P '<password>' -d '<database>' -i Schema.sql
```

# Correr el programa
Utilizando un sistema Ubuntu con .NET 5.0 se debe de utilizar lo siguiente:
```sh
cd WebServer
dotnet run
```

# Configuracion
Modificar WebServer/App.config para apuntar a el servidor con Microsoft SQL.

# Unit testing
El script de python hara unas pruebas a el sevidor para asegurarse que funcione correctamente

# Funcionalidad
* CRUD
* Unit-Testing con Python 3.7