param sqlServerName string

param databaseName string

param sharedResourceGroupName string

@secure()
param dbLogin string

@secure()
param dbPassword string

param location string = resourceGroup().location

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

// Variables
var hostingPlanName = 'plan-price-comparer-${location}-001'
var webSiteName = 'app-price-comparer-site-${location}-001'
var appInsightsName = 'appi-price-comparer-${location}-001'

// Web App resources
resource hostingPlan 'Microsoft.Web/serverfarms@2020-06-01' = {
  name: hostingPlanName
  location: location
  sku: {
    name: skuName
    capacity: skuCapacity
  }
}

resource sqlServer 'Microsoft.Sql/servers@2020-11-01-preview' existing  = {
  name: sqlServerName
  scope: resourceGroup(sharedResourceGroupName)
}

resource webSite 'Microsoft.Web/sites@2020-12-01' = {
  name: webSiteName
  location: location
  tags: {
    'hidden-related:${hostingPlan.id}': 'empty'
    displayName: 'Website'
  }
  properties: {
    serverFarmId: hostingPlan.id
    siteConfig:{
      netFrameworkVersion: 'v6.0'
    }    
  }
  identity: {
    type:'SystemAssigned'
  }
}

// resource bindings 'Microsoft.Web/sites/hostNameBindings@2021-02-01' = [for domain in domainNames:{
//   name: '${webSite.name}/${domain}'
//   properties: {
//     hostNameType: 'Verified'
//     siteName: webSite.name
//     sslState: 'Disabled'
//   }
// }]

resource webSiteConnectionStrings 'Microsoft.Web/sites/config@2020-06-01' = {
  name: '${webSite.name}/connectionstrings'
  properties: {
    DefaultConnection: {
      value: 'Data Source=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${databaseName};User Id=${dbLogin};Password=${dbPassword};'
      type: 'SQLAzure'
    }
  }
}

resource webSiteAppSettings 'Microsoft.Web/sites/config@2020-06-01' = {
  name: '${webSite.name}/appsettings'
  properties: {
    'APPLICATIONINSIGHTS_CONNECTION_STRING': appInsights.properties.ConnectionString    
  }
}

// Monitor
resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  tags: {
    'hidden-link:${webSite.id}': 'Resource'
    displayName: 'AppInsightsComponent'
  }
  kind: 'web'
  properties: {
    Application_Type: 'web'
  }
}

output webSiteName string = webSite.name
output webSiteDefaultHostName string = webSite.properties.defaultHostName
output appServicePlanName string = hostingPlan.name
