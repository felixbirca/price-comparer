az deployment sub create \
  --name price-comparer-dev \
  --location westeurope \
  --template-file main.bicep \
  --parameters location=westeurope \
    sqlServerName=sql-price-comparer-shared-nonprod \
    keyVaultName=kv-price-comparer-shared-nonprod \
    skuName=F1 \
