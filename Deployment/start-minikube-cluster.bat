

echo "Execute 'minikube start' to make cluster running"

kubectl apply -f cluster-photoprint-api.yml

kubectl apply -f loadbalancer-photoprint-api.yml

echo "Start 'minikube tunnel' in separate window to make the LB and deployment been exposed"



