@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

rem Set variables
set prediction_url="https://langservicejv.cognitiveservices.azure.com/language/:query-knowledgebases?projectName=Labb1-QnA&api-version=2021-10-01&deploymentName=production"
set key="ae45f22581574927865a0c5172d62893"

curl -X POST !prediction_url! -H "Ocp-Apim-Subscription-Key: !key!" -H "Content-Type: application/json" -d "{'question': '%*'}" | jq .answers[0].answer