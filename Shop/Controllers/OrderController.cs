﻿using Microsoft.AspNetCore.Mvc;
using Shop.Data.Interfaces;
using Shop.Data.Models;

namespace Shop.Controllers;

public class OrderController : Controller
{
	private readonly IAllOrders _allOrders;
	private readonly ShopCart _shopCart;

	public OrderController(IAllOrders allOrders, ShopCart shopCart)
	{
		_allOrders = allOrders;
		_shopCart = shopCart;
	}

	public IActionResult Checkout()
	{
		return View();
	}

	[HttpPost]
	public IActionResult Checkout(Order order)
	{
		_shopCart.listShopItems = _shopCart.getShopItems();

		if (_shopCart.listShopItems.Count == 0)
		{
			ModelState.AddModelError("Name", "У вас должны быть товары!");
		}

		//ModelState.Remove("orderDetails");
		if (ModelState.IsValid)
		{
			_allOrders.CreateOrder(order);
			return RedirectToAction("Complete");
		}

		return View(order);
	}

	public IActionResult Complete()
	{
		ViewBag.Message = "Заказ успешно отправлен";

		return View();
	}
}
