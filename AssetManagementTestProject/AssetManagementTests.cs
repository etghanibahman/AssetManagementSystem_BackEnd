using AssetManagementBusiness;
using AssetManagementData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace AssetManagementTestProject
{
    public class AssetManagementTests
    {
        private readonly AssetManagementService _assetManagementService;
        //AssetDbContext context;
        public AssetManagementTests()
        {
        }

        [Fact]
        public async Task GetAllAsync_WhenHasData_ShouldReturnListOfAssets()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
                .UseInMemoryDatabase(databaseName: "AssetDatabase1")
                .Options;
            using (var context = new AssetDbContext(options))
            {
                for (int i = 1; i < 4; i++)
                {
                    context.Assets.Add(new AssetModel()
                    {
                        AssetID = i,
                        AssetName = $"AssetName_{i}",
                        ImageFile = null,
                        ImageName = $"ImageName_{i}",
                        ImageSrc = $"ImageSrc_{i}"
                    });
                }
                context.SaveChanges();
                var assetManagementService = new AssetManagementService(context);
                
                //Act
                var res = await assetManagementService.GetAllAsync();

                //Assert
                Assert.True(res.Any());
            }
        }

        [Fact]
        public async Task GetByIdAsync_WhenHasData_ShouldReturnOneAssets()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
                .UseInMemoryDatabase(databaseName: "AssetDatabase2")
                .Options;

            using (var context = new AssetDbContext(options))
            {
                for (int i = 1; i < 4; i++)
                {
                    context.Assets.Add(new AssetModel()
                    {
                        AssetID = i,
                        AssetName = $"AssetName_{i}",
                        ImageFile = null,
                        ImageName = $"ImageName_{i}",
                        ImageSrc = $"ImageSrc_{i}"
                    });
                }
                context.SaveChanges();
                int idThatExist = 1;
                var assetManagementService = new AssetManagementService(context);

                //Act
                var res = await assetManagementService.GetByIdAsync(idThatExist);

                //Assert
                Assert.True(res != null);
            }
        }

        [Fact]
        public async Task GetByIdAsync_WhenDoesntHaveData_ShouldNotReturnOneAssets()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
                .UseInMemoryDatabase(databaseName: "AssetDatabase3")
                .Options;

            using (var context = new AssetDbContext(options))
            {
                for (int i = 1; i < 4; i++)
                {
                    context.Assets.Add(new AssetModel()
                    {
                        AssetID = i,
                        AssetName = $"AssetName_{i}",
                        ImageFile = null,
                        ImageName = $"ImageName_{i}",
                        ImageSrc = $"ImageSrc_{i}"
                    });
                }
                context.SaveChanges();
                var assetManagementService = new AssetManagementService(context);
                int idThatDoesntExist = 10;

                //Act
                var res = await assetManagementService.GetByIdAsync(idThatDoesntExist);

                //Assert
                Assert.False(res != null);
            }         
        }

        [Fact]
        public async Task DeleteAsync_WhenHasData_ShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
                .UseInMemoryDatabase(databaseName: "AssetDatabase4")
                .Options;

            using (var context = new AssetDbContext(options))
            {
                for (int i = 1; i < 4; i++)
                {
                    context.Assets.Add(new AssetModel()
                    {
                        AssetID = i,
                        AssetName = $"AssetName_{i}",
                        ImageFile = null,
                        ImageName = $"ImageName_{i}",
                        ImageSrc = $"ImageSrc_{i}"
                    });
                }
                context.SaveChanges();
                int idThatExist = 1;
                var assetManagementService = new AssetManagementService(context);

                //Act
                var res = await assetManagementService.DeleteAsync(idThatExist);

                //Assert
                Assert.True(res);
            }
        }

        [Fact]
        public async Task DeleteAsync_WhenDoesntHaveData_ShouldReturnFalse()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
                .UseInMemoryDatabase(databaseName: "AssetDatabase5")
                .Options;

            using (var context = new AssetDbContext(options))
            {
                for (int i = 1; i < 4; i++)
                {
                    context.Assets.Add(new AssetModel()
                    {
                        AssetID = i,
                        AssetName = $"AssetName_{i}",
                        ImageFile = null,
                        ImageName = $"ImageName_{i}",
                        ImageSrc = $"ImageSrc_{i}"
                    });
                }
                context.SaveChanges();
                int idThatDoesntExist = 10;
                var assetManagementService = new AssetManagementService(context);

                //Act
                var res = await assetManagementService.DeleteAsync(idThatDoesntExist);

                //Assert
                Assert.False(res);
            }
        }


        [Fact]
        public async Task CreateAsync_WhenCorrectDataIsAdded_ShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
                .UseInMemoryDatabase(databaseName: "AssetDatabase6")
                .Options;

            using (var context = new AssetDbContext(options))
            {
                for (int i = 1; i < 4; i++)
                {
                    context.Assets.Add(new AssetModel()
                    {
                        AssetID = i,
                        AssetName = $"AssetName_{i}",
                        ImageFile = null,
                        ImageName = $"ImageName_{i}",
                        ImageSrc = $"ImageSrc_{i}"
                    });
                }
                context.SaveChanges();

                var newAsset = new AssetModel()
                {
                    AssetName = $"AssetName_New",
                    ImageFile = null,
                    ImageName = $"ImageName_New",
                    ImageSrc = $"ImageSrc_New"
                };
                var assetManagementService = new AssetManagementService(context);

                //Act
                var res = await assetManagementService.CreateAsync(newAsset);

                //Assert
                Assert.True(res);
            }
        }


        [Fact]
        public async Task Update_WhenExistingDataIsEdited_ShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<AssetDbContext>()
                .UseInMemoryDatabase(databaseName: "AssetDatabase7")
                .Options;

            using (var context = new AssetDbContext(options))
            {
                for (int i = 1; i < 4; i++)
                {
                    context.Assets.Add(new AssetModel()
                    {
                        AssetID = i,
                        AssetName = $"AssetName_{i}",
                        ImageFile = null,
                        ImageName = $"ImageName_{i}",
                        ImageSrc = $"ImageSrc_{i}"
                    });
                }
                context.SaveChanges();

                var editedAsset = context.Assets.FirstOrDefault();
                editedAsset.AssetName = "Edited";

                var assetManagementService = new AssetManagementService(context);

                //Act
                var res = await assetManagementService.Update(editedAsset);

                //Assert
                Assert.True(res);
            }
        }
    }
}
