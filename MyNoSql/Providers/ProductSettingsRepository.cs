﻿using DataServices.MyNoSql.Interfaces.ProductSettings;
using DataServices.MyNoSql.Models.ProductSettings;
using MyNoSqlServer.Abstractions;
using MyNoSqlServer.DataWriter;

namespace DataServices.MyNoSql.Providers;

public class ProductSettingsRepository : IProductSettingsRepository
{
    private readonly IMyNoSqlServerDataWriter<ProductBrandSettingsEntity> _brandTable;
    private readonly IMyNoSqlServerDataWriter<ProductRecaptchaSettingsEntity> _recaptchaTable;
    private readonly IMyNoSqlServerDataWriter<ProductTrackboxSettingsEntity> _trackboxTable;
    private const string TableName = "product-settings";
    public ProductSettingsRepository(string url)
    {
        var brandTable = new MyNoSqlServerDataWriter<ProductBrandSettingsEntity>(
            () => url,
            TableName,
            true);
        // create table if not exists
        brandTable.CreateTableIfNotExistsAsync().GetAwaiter().GetResult();
        _brandTable = brandTable;
        _recaptchaTable = new MyNoSqlServerDataWriter<ProductRecaptchaSettingsEntity>(
            () => url,
            TableName,
            true);
        _trackboxTable = new MyNoSqlServerDataWriter<ProductTrackboxSettingsEntity>(
            () => url,
            TableName,
            true);
    }

    public async Task<IProductBrandSettings> GetBrandSettingsAsync()
    {
        return await _brandTable.GetAsync(ProductBrandSettingsEntity.GeneratePartitionKey(), "brand");
    }

    public async void SetBrandSettingsAsync(IProductBrandSettings settings)
    {
        var entity = ProductBrandSettingsEntity.Create(settings);
        await _brandTable.InsertOrReplaceAsync(entity);
    }

    public async Task<IProductRecaptchaSettings> GetRecaptchaSettingsAsync()
    {
        return await _recaptchaTable.GetAsync(ProductRecaptchaSettingsEntity.GeneratePartitionKey(), "recaptcha");
    }

    public async void SetRecaptchaSettingsAsync(IProductRecaptchaSettings settings)
    {
		var entity = ProductRecaptchaSettingsEntity.Create(settings);
		await _recaptchaTable.InsertOrReplaceAsync(entity);
    }

    public async Task<IProductTrackboxSettings> GetTrackboxSettingsAsync()
    {
        return await _trackboxTable.GetAsync(ProductTrackboxSettingsEntity.GeneratePartitionKey(), "trackbox");
    }

    public async void SetTrackboxSettingsAsync(IProductTrackboxSettings settings)
    {
	    var entity = ProductTrackboxSettingsEntity.Create(settings);
	    await _trackboxTable.InsertOrReplaceAsync(entity);
	}
}