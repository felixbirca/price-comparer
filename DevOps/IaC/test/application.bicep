param location string

// variables
var hostingPlanName = 'plan-price-comparer-${location}-001'
var webSiteName = 'app-price-comparer-site-${location}-001'

resource hostingPlan 'Microsoft.Web/serverfarms@2018-02-01' = {
  name: hostingPlanName
  location: location
  sku: {
    name: 'F1'
    capacity: 1
  }
}


resource webSite 'Microsoft.Web/sites@2018-11-01' = {
  name: webSiteName
  location: location
  tags: {
  }
  properties: {
    siteConfig: {
      appSettings: []
      netFrameworkVersion: 'v6.0'
    }
    serverFarmId: hostingPlan.id
    clientAffinityEnabled: true
    httpsOnly: true
  }
  dependsOn: [
    hostingPlan
  ]
}

