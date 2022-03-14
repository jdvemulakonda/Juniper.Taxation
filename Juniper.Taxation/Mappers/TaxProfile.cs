using System.ComponentModel.Design;
using AutoMapper;
using Juniper.Taxation.Core.Application.Models;
using Juniper.Taxation.Core.Domain.Entities;
using TaxJarContracts=Juniper.Taxation.Infrastructure.Providers.TaxJar.Contracts;

namespace Juniper.Taxation.Mappers
{
    public class TaxProfile:Profile
    {
        public TaxProfile()
        {
            CreateMap<OrderSalesTaxCommand, TaxJarContracts.CalculateSalesTaxOrderRequest>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(destination => destination.amount, opt =>
                {
                    opt.MapFrom(src => src.Amount);
                })
                .ForMember(destination => destination.shipping, opt =>
                {
                    opt.MapFrom(src => src.Shipping);
                })
                .ForMember(destination => destination.from_zip, opt =>
                {
                    opt.MapFrom(src => src.FromAddress.Zip);
                })
                .ForMember(destination => destination.from_city, opt =>
                {
                    opt.MapFrom(src => src.FromAddress.City);
                })
                .ForMember(destination => destination.from_state, opt =>
                {
                    opt.MapFrom(src => src.FromAddress.State);
                })
                .ForMember(destination => destination.from_country, opt =>
                {
                    opt.MapFrom(src => src.FromAddress.Country);
                })
                .ForMember(destination => destination.from_street, opt =>
                {
                    opt.MapFrom(src => src.ToAddress.Street);
                })
                .ForMember(destination => destination.to_street, opt =>
                {
                    opt.MapFrom(src => src.ToAddress.Zip);
                })
                .ForMember(destination => destination.to_city, opt =>
                {
                    opt.MapFrom(src => src.ToAddress.City);
                })
                .ForMember(destination => destination.to_state, opt =>
                {
                    opt.MapFrom(src => src.ToAddress.State);
                })
                .ForMember(destination => destination.to_country, opt =>
                {
                    opt.MapFrom(src => src.ToAddress.Country);
                })
                .ForMember(destination => destination.to_zip, opt =>
                {
                    opt.MapFrom(src => src.ToAddress.Zip);
                });

           
            CreateMap<TaxJarContracts.SalesTax,SalesTax>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(destination => destination.Rate,
                    opt =>
                        opt.MapFrom(src => src.Rate))
                .ForMember(destination => destination.OrderTotalAmount,
                    opt =>
                        opt.MapFrom(src => src.Order_total_amount))
                .ForMember(destination => destination.AmountToCollect,
                    opt =>
                        opt.MapFrom(src => src.Amount_to_collect))
                .ForMember(destination => destination.Shipping,
                    opt =>
                        opt.MapFrom(src => src.Shipping))
                .ForMember(destination => destination.TaxSource,
                    opt =>
                        opt.MapFrom(src => src.Tax_source))
                .ForMember(destination => destination.FreightTaxable,
                    opt =>
                        opt.MapFrom(src => src.Freight_taxable));


            CreateMap<TaxJarContracts.CalculateSalesTaxOrderResponse, SalesTaxByOrderResponse>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(destination => destination.SalesTax,
                    opt =>
                        opt.MapFrom(src => src.SalesTax));
                
            CreateMap<TaxJarContracts.Taxrate,TaxRate>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(destination => destination.CombinedRate, opt =>
                    opt.MapFrom(src => src.combined_rate))
                .ForMember(destination => destination.City, opt =>
                    opt.MapFrom(src => src.city))
                .ForMember(destination => destination.CityRate, opt =>
                    opt.MapFrom(src => src.city_rate))
                .ForMember(destination => destination.County, opt =>
                    opt.MapFrom(src => src.county))
                .ForMember(destination => destination.CountyRate, opt =>
                    opt.MapFrom(src => src.county_rate))
                .ForMember(destination => destination.CombinedDistrictRate, opt =>
                    opt.MapFrom(src => src.combined_district_rate))
                .ForMember(destination => destination.State, opt =>
                    opt.MapFrom(src => src.state))
                .ForMember(destination => destination.StateRate, opt =>
                    opt.MapFrom(src => src.state_rate))
                .ForMember(destination => destination.FreightTaxable, opt =>
                    opt.MapFrom(src => src.freight_taxable))
                .ForMember(destination => destination.Zip, opt =>
                    opt.MapFrom(src => src.zip));

            CreateMap<TaxJarContracts.GetTaxRateByLocationResponse, GetTaxRateByLocationResponse>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(destination => destination.Rate, opt =>
                    opt.MapFrom(src => src.rate));


        }


    }
}