# Fora.Challenge

## Overview

`Fora.Challenge` is a RESTful API designed to manage company data efficiently.  
This service provides endpoints for importing and retrieving company data, integrates with external APIs, and includes robust logging and error handling.

---

## Getting Started

### Prerequisites

To run the service locally, ensure you have:
- [.NET 8 SDK](https://dotnet.microsoft.com/)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

---

## Usage

- First, you will want to import data from the edger api to the database with `/api/v1/companies/bulk`
- Then you can fetch company data by using `/api/v1/companies`

### API Endpoints

#### GET /api/v1/companies
- Fetch company data.
- filter for companies with first letter (`/api/v1/companies?filter=A`)

#### POST /api/v1/companies/bulk
Save company data in bulk.
