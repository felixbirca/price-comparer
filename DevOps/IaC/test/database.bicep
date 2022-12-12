param location string = resourceGroup().location

param sqlServerName string

var databaseName = 'price-comparer-database-001'

resource database 'Microsoft.Sql/servers/databases@2021-11-01' = {
  name: '${sqlServerName}/${databaseName}'
  location: location
  sku: {
    name: 'Basic'
    tier: 'Basic'
    capacity: 5
    size: any('0.5')
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 1073741824
  }
}

output databaseName string = databaseName
