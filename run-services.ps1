# Step 1: Run "docker compose up -d"
Write-Output "Running Docker Compose..."
docker compose up -d

# Step 2: Navigate to "src/GatewayService" and run "dotnet watch" in a new terminal
Write-Output "Starting GatewayService..."
Start-Process powershell -ArgumentList "cd src/GatewayService; dotnet watch"

# Step 3: Navigate to "src/IdentityService" and run "dotnet watch" in a new terminal
Write-Output "Starting IdentityService..."
Start-Process powershell -ArgumentList "cd src/IdentityService; dotnet watch"

# Step 4: Navigate to "src/AuctionService" and run "dotnet watch" in a new terminal
Write-Output "Starting AuctionService..."
Start-Process powershell -ArgumentList "cd src/AuctionService; dotnet watch"

# Step 5: Navigate to "src/SearchService" and run "dotnet watch" in a new terminal
Write-Output "Starting SearchService..."
Start-Process powershell -ArgumentList "cd src/SearchService; dotnet watch"

Write-Output "All services are started."
