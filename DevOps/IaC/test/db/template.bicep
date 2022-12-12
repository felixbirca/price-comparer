param administratorLogin string = ''

@secure()
param administratorLoginPassword string = ''
param administrators object = {
}
param location string
param serverName string
param enableADS bool = false

@description('To enable vulnerability assessments, the user deploying this template must have an administrator or owner permissions.')
param useVAManagedIdentity bool = false
param allowAzureIps bool = true
param enableVA bool = false
param serverTags object = {
}

var subscriptionId = subscription().subscriptionId
var resourceGroupName = resourceGroup().name
var uniqueStorage = uniqueString(subscriptionId, resourceGroupName, location)
var storageName = toLower('sqlva${uniqueStorage}')
var uniqueRoleGuid = guid(storage.id, StorageBlobContributor, server.id)
var StorageBlobContributor = subscriptionResourceId('Microsoft.Authorization/roleDefinitions', 'ba92f5b4-2d11-453d-a403-e96b0029c9fe')

resource storage 'Microsoft.Storage/storageAccounts@2019-04-01' = if (enableVA) {
  name: storageName
  location: location
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    minimumTlsVersion: 'TLS1_2'
    supportsHttpsTrafficOnly: 'true'
    allowBlobPublicAccess: 'false'
  }
}

resource storageName_Microsoft_Authorization_uniqueRoleGuid 'Microsoft.Storage/storageAccounts/providers/roleAssignments@2018-09-01-preview' = if (enableVA) {
  name: '${storageName}/Microsoft.Authorization/${uniqueRoleGuid}'
  properties: {
    roleDefinitionId: StorageBlobContributor
    principalId: reference(server.id, '2018-06-01-preview', 'Full').identity.principalId
    scope: storage.id
    principalType: 'ServicePrincipal'
  }
}

resource server 'Microsoft.Sql/servers@2020-11-01-preview' = {
  name: serverName
  location: location
  properties: {
    version: '12.0'
    administratorLogin: administratorLogin
    administratorLoginPassword: administratorLoginPassword
  }
  identity: ((enableVA && useVAManagedIdentity) ? json('{"type":"SystemAssigned"}') : json('null'))
  tags: serverTags
}

resource serverName_AllowAllWindowsAzureIps 'Microsoft.Sql/servers/firewallRules@2021-11-01' = if (allowAzureIps) {
  parent: server
  name: 'AllowAllWindowsAzureIps'
  location: location
  properties: {
    endIpAddress: '0.0.0.0'
    startIpAddress: '0.0.0.0'
  }
}

resource serverName_Default 'Microsoft.Sql/servers/advancedThreatProtectionSettings@2021-11-01-preview' = if (enableADS) {
  parent: server
  name: 'Default'
  properties: {
    state: 'Enabled'
  }
}

resource Microsoft_Sql_servers_vulnerabilityAssessments_serverName_Default 'Microsoft.Sql/servers/vulnerabilityAssessments@2018-06-01-preview' = if (enableVA) {
  parent: server
  name: 'Default'
  properties: {
    storageContainerPath: (enableVA ? '${storage.properties.primaryEndpoints.blob}vulnerability-assessment' : '')
    storageAccountAccessKey: ((enableVA && (!useVAManagedIdentity)) ? listKeys(storageName, '2018-02-01').keys[0].value : '')
    recurringScans: {
      isEnabled: true
      emailSubscriptionAdmins: true
    }
  }
  dependsOn: [

    serverName_Default
  ]
}