param ($name, $rg)
az resource delete --resource-group $rg --name $name --resource-type "Microsoft.Storage/storageAccounts"
