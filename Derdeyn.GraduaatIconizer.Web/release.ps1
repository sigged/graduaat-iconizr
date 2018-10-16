dotnet publish -c Release
cd .\bin\Release\netcoreapp2.1\publish\
heroku container:login
heroku container:push web -a graduaat-iconizr
heroku container:release web -a graduaat-iconizr