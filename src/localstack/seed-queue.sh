#!/bin/bash

echo "Running queue seed..."

# valid ones
aws --endpoint-url=http://localhost:4566 sqs send-message --queue-url http://localhost:4566/000000000000/snacktech-processed-payments --message-body '{"PedidoId": "e0882757-1bda-4884-9861-ba8d9e2e7869", "DataRecebimento": "2025-01-01T12:00:00Z", "PagamentoId": "6c5250e1-5843-42b9-bce7-224f0cbf01a9", "NomePlataforma": "Plataforma Teste"}'
aws --endpoint-url=http://localhost:4566 sqs send-message --queue-url http://localhost:4566/000000000000/snacktech-processed-payments --message-body '{"PedidoId": "e0882757-2bda-4884-9861-ba8d9e2e7869", "DataRecebimento": "2025-01-01T12:00:00Z", "PagamentoId": "6c5250e1-5843-42b9-bce7-224f0cbf01a9", "NomePlataforma": "Plataforma Teste"}' 

# invalids one
aws --endpoint-url=http://localhost:4566 sqs send-message --queue-url http://localhost:4566/000000000000/snacktech-processed-payments --message-body '{"PedidoId": "e0882757-2bda-4884-9861-ba8d9e2e7869", "DataRecebimento": "2024-01-01T12:00:00Z", "PagamentoId": "6c5250e1-5843-42b9-bce7-224f0cbf01a9", "NomePlataforma": "Plataforma Teste"}'
aws --endpoint-url=http://localhost:4566 sqs send-message --queue-url http://localhost:4566/000000000000/snacktech-processed-payments --message-body '{"PedidoId": "e0882757-2bda-4884-0061-ba8d9e2e7869", "DataRecebimento": "2025-01-01T12:00:00Z", "PagamentoId": "6c5250e1-5843-42b9-bce7-224f0cbf01a9", "NomePlataforma": "Plataforma Teste"}'

echo "Queue seed complete."

# kill the container
exit 0