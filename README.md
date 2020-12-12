WebCrawler is a Web API app that demonstrates the performance differences between hitting URLs synchronously and asynchronously. It also demonstrates the use of Docker and Kubernetes (with Azure Kubernetes Service), which is defined as Infrastructure as Code in the ARM template. It also demonstrates using Azure DevOps Pipelines to automate deployment to Azure Kubernetes Service by CI/CD.


To run WebCrawler using Kestrel locally:
use cmd: dotnet run

Navigate here:
http://localhost:5000/api/crawler/crawlasynchronously
https://localhost:5001/api/crawler/crawlasynchronously

Running from Docker locally:

docker build -t wcc .
docker run -p 3000:80 wcc


Instead of using the ARM Template to deploy stuff into the webcrawler-rg resource group, you can run this from the Azure CLI!
See if something like this can be Infrastructure as Code!

# Create a resource group
az group create --name webcrawler-rg --location eastus

# Create a container registry
az acr create --resource-group webcrawler-rg --name WebCrawlerContainerRegistry --sku Basic

# Create a Kubernetes cluster
az aks create --resource-group webcrawler-rg --name WebCrawler --node-count 1 --enable-addons monitoring --generate-ssh-keys --kubernetes-version 1.19.1

In parameters file, you must set "sshRSAPublicKey".
Run this in the command prompt:
ssh-keygen -t rsa -b 2048
Copy and paste the public part of your SSH key pair (by default, the contents of ~/.ssh/id_rsa.pub).
For more details:
https://docs.microsoft.com/en-us/azure/aks/kubernetes-walkthrough-rm-template

Related resources:
To run kubectl locally connected to AKS:
https://zero-to-jupyterhub.readthedocs.io/en/latest/kubernetes/microsoft/step-zero-azure.html

https://docs.microsoft.com/en-us/azure/devops/pipelines/ecosystems/kubernetes/aks-template?view=azure-devops
https://azuredevopslabs.com/labs/devopsserver/armtemplates/
https://resources.azure.com/providers/Microsoft.ContainerRegistry/operations
https://www.razorspoint.com/2018/06/21/5-tips-for-developing-arm-templates/

