
WebCrawler
README.md

To run WebCrawler using Kestrel locally:
use cmd: dotnet run

Navigate here:
http://localhost:5000/api/crawler/crawlasynchronously
https://localhost:5001/api/crawler/crawlasynchronously

Running from Docker locally:

docker build -t wcc .
docker run -p 3000:80 wcc


Instead of using the big generated ARM Template to deploy stuff into the webcrawler-rg resource group, running this from the Azure CLI is much easier!
See if something like this can be Infrastructure as Code!

# Create a resource group
az group create --name webcrawler-rg --location eastus

# Create a container registry
az acr create --resource-group webcrawler-rg --name WebCrawlerContainerRegistry --sku Basic

# Create a Kubernetes cluster
az aks create --resource-group webcrawler-rg --name WebCrawler --node-count 1 --enable-addons monitoring --generate-ssh-keys --kubernetes-version 1.19.1

To run kubectl locally connected to AKS:
https://zero-to-jupyterhub.readthedocs.io/en/latest/kubernetes/microsoft/step-zero-azure.html

https://docs.microsoft.com/en-us/azure/devops/pipelines/ecosystems/kubernetes/aks-template?view=azure-devops
https://azuredevopslabs.com/labs/devopsserver/armtemplates/
https://resources.azure.com/providers/Microsoft.ContainerRegistry/operations
https://www.razorspoint.com/2018/06/21/5-tips-for-developing-arm-templates/

