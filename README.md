
cd .\MyPaymentService

docker build -t payment-service:v1 .
docker run --name payment-service-container -p 32770:8080 -e ASPNETCORE_URLS=http://+:8080 payment-service:v1
docker run --name payment-service-container -p 32770:8080 -e ASPNETCORE_URLS=http://+:8080 -e UseAzureConfig=false -e Payment__MaxAmount=10000 -e PaymentGateway__ApiKey=local-secret payment-service:v1