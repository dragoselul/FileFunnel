# Use the official .NET 9 SDK image as base
FROM mcr.microsoft.com/vscode/devcontainers/dotnet:9.0

# Install SkiaSharp deps
RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    libc6 \
    libexpat1 \
    libuuid1 \
    zlib1g \
    libx11-6 \
    libx11-dev \
    libxext6 \
    libxrandr2 \
    libxfixes3 \
    libxi6 \
    libxcursor1 \
    libxrender1 \
    libxtst6 \
    libglib2.0-0 \
    libgtk-3-0 \
    libice6 \
    libsm6 \
  && rm -rf /var/lib/apt/lists/*

