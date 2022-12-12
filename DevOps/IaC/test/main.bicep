targetScope='subscription'

param location string

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-01-01' = {
  name: 'padlab3-felix'
  location: location
}

module keyvault 'keyvault.bicep' = {
  scope: resourceGroup
  name: 'keyVault'
  params: {
    tenantId: subscription().tenantId
    location: location
  }
}

module application 'application.bicep' = {
  scope: resourceGroup
  name: 'applicationResources'
  params: {
    location: location
  }
}
