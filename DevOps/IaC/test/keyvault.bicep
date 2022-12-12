param tenantId string

param location string = resourceGroup().location

var keyVaultName = 'price-comparer-kv'

resource kv 'Microsoft.KeyVault/vaults@2019-09-01' = {
  name: keyVaultName
  location: location
  properties:{
    sku:{
      family: 'A'
      name: 'standard'
    }
    tenantId: tenantId
    enabledForTemplateDeployment: true
    accessPolicies:[
    ]
  }

}

output keyVaultName string = kv.name
output keyVaultId string = kv.id
