#!/bin/sh

echo "Running LocalStack initialization..."

awslocal sqs create-queue --queue-name snacktech-processed-payments 
awslocal sqs create-queue --queue-name snacktech-processed-payments-dlq

echo "LocalStack initialization complete."
