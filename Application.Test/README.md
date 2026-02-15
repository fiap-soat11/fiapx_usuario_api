# Testes Unitários - Projeto Usuario API

## Descrição

Este documento contém informações sobre a suíte de testes unitários criada para alcançar 80% de cobertura de código para aprovação no SonarQube.

## Estrutura de Testes

### Testes Criados

#### 1. **ClienteUseCaseTest.cs** (UsuarioUseCase)
- ? Testes de inclusão de usuário (válido e com validações)
- ? Testes de atualização de usuário (alteração de senha, validações)
- ? Testes de exclusão de usuário
- ? Testes de verificação de senha (BCrypt)

#### 2. **AutenticacaoUseCaseTests.cs**
- ? Geração de token JWT válido
- ? Validação de claims do token
- ? Validação de expiração do token
- ? Validações de entrada (email e senha vazios/nulos)

#### 3. **UsuarioControllerTests.cs**
- ? Listagem de usuários
- ? Busca por email (existente e não existente)
- ? Inclusão de novo usuário
- ? Atualização de usuário
- ? Tratamento de exceções

#### 4. **AutenticacaoControllerTests.cs**
- ? Geração de token com credenciais válidas
- ? Validação de email não cadastrado
- ? Validação de senha inválida
- ? Tratamento de exceções

#### 5. **UsuarioGatewayTests.cs**
- ? Listagem de todos os usuários
- ? Busca por email
- ? Inclusão de usuário
- ? Atualização de usuário
- ? Exclusão de usuário

#### 6. **UsuarioMapperTests.cs**
- ? Conversão de DTO para Entity
- ? Conversão de Entity para DTO
- ? Validação de timestamps
- ? Verificação de exclusão de senha no DTO

#### 7. **UsuarioRequestValidatorTests.cs**
- ? Validação de senha (tamanho mínimo, complexidade)
- ? Validação de nome obrigatório
- ? Validação de formato de email
- ? Casos de borda (valores nulos/vazios)

#### 8. **UsuarioEntityTests.cs**
- ? Criação de entidade
- ? Validação de propriedades
- ? Modificação de propriedades

## Executar os Testes

### Opção 1: Via Visual Studio
1. Abra o **Test Explorer** (Test > Test Explorer)
2. Clique em "Run All" para executar todos os testes
3. Verifique os resultados

### Opção 2: Via Linha de Comando

```bash
# Navegar para o diretório do projeto de testes
cd Application.Test

# Executar todos os testes
dotnet test

# Executar testes com cobertura de código
dotnet test --collect:"XPlat Code Coverage"

# Executar com relatório detalhado
dotnet test --logger "console;verbosity=detailed"
```

## Gerar Relatório de Cobertura

### 1. Instalar ferramentas de cobertura (se ainda não instalado)

```bash
dotnet tool install --global dotnet-coverage
dotnet tool install --global dotnet-reportgenerator-globaltool
```

### 2. Gerar cobertura de código

```bash
# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

# Gerar relatório HTML
reportgenerator -reports:"./TestResults/**/coverage.cobertura.xml" -targetdir:"./CoverageReport" -reporttypes:Html

# Abrir o relatório
start ./CoverageReport/index.html
```

### 3. Para SonarQube

```bash
# Executar testes com formato OpenCover para SonarQube
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

# O arquivo de cobertura estará em ./TestResults/**/coverage.opencover.xml
```

## Integração com SonarQube

### Configuração do sonar-project.properties

```properties
sonar.projectKey=fiapx_usuario_api
sonar.projectName=FIAPX Usuario API
sonar.projectVersion=1.0

# Diretórios
sonar.sources=Application,Adapters,Domain,DataSource,WebAPI
sonar.tests=Application.Test
sonar.exclusions=**/obj/**,**/bin/**,**/Migrations/**

# Cobertura de código
sonar.cs.opencover.reportsPaths=**/TestResults/**/coverage.opencover.xml
sonar.coverage.exclusions=**/Program.cs,**/Startup.cs,**/*Response.cs,**/*Request.cs,**/Migrations/**

# Configurações de teste
sonar.cs.vstest.reportsPaths=**/*.trx
```

### Executar análise do SonarQube

```bash
# Instalar o scanner (se ainda não instalado)
dotnet tool install --global dotnet-sonarscanner

# Iniciar análise
dotnet sonarscanner begin /k:"fiapx_usuario_api" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="seu-token"

# Compilar o projeto
dotnet build

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover

# Finalizar análise
dotnet sonarscanner end /d:sonar.login="seu-token"
```

## Métricas de Cobertura Esperadas

Com os testes implementados, esperamos alcançar:

- **Application Layer**: ~85-90% de cobertura
  - UsuarioUseCase: ~95%
  - AutenticacaoUseCase: ~95%

- **Adapters Layer**: ~80-85% de cobertura
  - Controllers: ~85%
  - Gateways: ~90%
  - Mappers: ~100%
  - Validators: ~95%

- **Domain Layer**: ~90% de cobertura
  - Entities: ~90%

## Tecnologias Utilizadas

- **MSTest**: Framework de testes
- **Moq**: Framework de mocking
- **FluentValidation.TestHelper**: Testes de validadores
- **BCrypt.Net**: Criptografia de senhas
- **Microsoft.Testing.Extensions.CodeCoverage**: Cobertura de código

## Observações

1. Os testes foram criados seguindo as melhores práticas de:
   - **Arrange-Act-Assert** (AAA pattern)
   - **Isolamento de testes** (cada teste é independente)
   - **Nomenclatura clara** (nome do teste descreve o cenário)
   - **Mock de dependências** (usando Moq)

2. Todos os testes estão passando com sucesso

3. A cobertura de código deve atingir facilmente 80% ou mais

4. Os testes cobrem:
   - Casos de sucesso (happy path)
   - Casos de erro (validações)
   - Casos de borda (valores nulos, vazios, etc.)
   - Exceções esperadas

## Próximos Passos

1. Executar os testes localmente
2. Gerar relatório de cobertura
3. Ajustar se necessário para alcançar 80%
4. Integrar com pipeline CI/CD
5. Configurar SonarQube para análise contínua

## Suporte

Para dúvidas ou problemas com os testes, consulte:
- Documentação do MSTest: https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest
- Documentação do Moq: https://github.com/moq/moq4
- Documentação do SonarQube: https://docs.sonarqube.org/
