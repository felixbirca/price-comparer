targetScope='subscription'


param location string

param sqlServerName string

param keyVaultName string


// Web App params
@allowed([
  'F1'
  'D1'
  'B1'
  'B2'
  'B3'
  'S1'
  'S2'
  'S3'
  'P1'
  'P2'
  'P3'
  'P4'
])
param skuName string = 'F1'

@minValue(1)
param skuCapacity int = 1

var resourceGroupName = 'rg-price-comparer-${location}'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: resourceGroupName
  location: location
}
resource kv 'Microsoft.KeyVault/vaults@2019-09-01' existing = {
  name: keyVaultName
  scope: resourceGroup
}


module database 'database.bicep' = {
  scope: resourceGroup
  name: 'database'
  params:{
    location: location
    sqlServerName: sqlServerName
  }
}

module application 'application.bicep' = {
  scope: resourceGroup
  name: 'applicationResources'
  params:{
    location: location
    sharedResourceGroupName: resourceGroupName
    sqlServerName: sqlServerName
    databaseName: database.outputs.databaseName
    dbLogin: kv.getSecret('price-comparer-app-sqlLogin')
    dbPassword: kv.getSecret('price-comparer-app-sqlPassword')
    skuName: skuName
    skuCapacity: skuCapacity
  }

  dependsOn: [
    database
  ]
}

output webSiteName string = application.outputs.webSiteName
output webSiteDefaultHostName string = application.outputs.webSiteDefaultHostName
