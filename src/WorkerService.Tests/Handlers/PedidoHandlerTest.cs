// using Microsoft.Extensions.Logging;
// using Moq;
// using WorkerService.Data.Entities;
// using WorkerService.Data.Repository;
// using WorkerService.DTOs;
// using WorkerService.Enums;

// namespace WorkerService.Handlers.Tests
// {
//     public class PedidoHandlerTests
//     {
//         private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
//         private readonly IPedidoHandler _handler;

//         public PedidoHandlerTests()
//         {
//             _pedidoRepositoryMock = new Mock<IPedidoRepository>();
//             var mockLogger = new Mock<ILogger<PedidoHandler>>();
//             _handler = new PedidoHandler(mockLogger.Object, _pedidoRepositoryMock.Object);
//         }

//         [Fact]
//         public async Task DeveProcessarPedidoComSucesso()
//         {
//             // Arrange
//             var mensagem = new MensagemPedidoDto
//             {
//                 PedidoId = Guid.NewGuid(),
//                 PagamentoId = Guid.NewGuid(),
//                 DataRecebimento = DateTime.Now,
//                 NomePlataforma = "Plataforma Teste",
//             };

//             _pedidoRepositoryMock.Setup(r => r.GetByIdAsync(mensagem.PedidoId))
//                 .ReturnsAsync(new Pedido
//                 {
//                     Id = mensagem.PedidoId,
//                     DataCriacao = DateTime.Now,
//                     Status = 2 //TODO: trocar numero magico
//                 });

//             // Act
//             await _handler.ProcessarPedidoAsync(mensagem);

//             // Assert
//             _pedidoRepositoryMock.Verify(r => r.GetByIdAsync(mensagem.PedidoId), Times.Once);
//             _pedidoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Pedido>()), Times.Once);
//         }

//         [Fact]
//         public async Task DeveTratarQuandoPedidoNaoEncontrado()
//         {
//             // Arrange
//             var mensagem = new MensagemPedidoDto
//             {
//                 PedidoId = Guid.NewGuid(),
//                 PagamentoId = Guid.NewGuid(),
//                 DataRecebimento = DateTime.Now,
//                 NomePlataforma = "Plataforma Teste",
//             };

//             _pedidoRepositoryMock.Setup(r => r.GetByIdAsync(mensagem.PedidoId))
//                 .ReturnsAsync((Pedido?)null);

//             // Act
//             var exception = await Assert.ThrowsAsync<InvalidOperationException>(
//                 () => _handler.ProcessarPedidoAsync(mensagem));

//             // Assert
//             Assert.Equal($"Pedido with ID {mensagem.PedidoId} não encontrado.", exception.Message);
//         }

//         [Fact]
//         public async Task DeveTratarQuandoDataPagamentoEhAnteriorAoPedido()
//         {
//             // Arrange
//             var mensagem = new MensagemPedidoDto
//             {
//                 PedidoId = Guid.NewGuid(),
//                 PagamentoId = Guid.NewGuid(),
//                 DataRecebimento = DateTime.Now.AddDays(-1),
//                 NomePlataforma = "Plataforma Teste",
//             };

//             _pedidoRepositoryMock.Setup(r => r.GetByIdAsync(mensagem.PedidoId))
//                 .ReturnsAsync(new Pedido
//                 {
//                     Id = mensagem.PedidoId,
//                     DataCriacao = DateTime.Now,
//                     Status = 2 //TODO: trocar numero magico
//                 });

//             // Act
//             var exception = await Assert.ThrowsAsync<InvalidOperationException>(
//                 () => _handler.ProcessarPedidoAsync(mensagem));

//             // Assert
//             Assert.Equal(
//                 $"DataModificacao in the message is older than UltimaAtualizacao for Pedido with ID {mensagem.PedidoId}.",
//                 exception.Message);
//         }

//         [Fact]
//         public async Task DeveTratarQuandoStatusPedidoEhInvalido()
//         {
//             // Arrange
//             var mensagem = new MensagemPedidoDto
//             {
//                 PedidoId = Guid.NewGuid(),
//                 StatusPedido = 99,
//                 DataModificacao = DateTime.Now
//             };

//             _pedidoRepositoryMock.Setup(r => r.GetByIdAsync(mensagem.PedidoId))
//                 .ReturnsAsync(new Pedido
//                 {
//                     Id = mensagem.PedidoId,
//                     DataCriacao = DateTime.Now,
//                     Status = 2 //TODO: trocar numero magico
//                 });

//             // Act
//             var exception = await Assert.ThrowsAsync<InvalidOperationException>(
//                 () => _handler.ProcessarPedidoAsync(mensagem));

//             // Assert
//             Assert.Equal(
//                 $"StatusPedido in the message is invalid for Pedido with ID {mensagem.PedidoId}.",
//                 exception.Message);
//         }
//     }
// }