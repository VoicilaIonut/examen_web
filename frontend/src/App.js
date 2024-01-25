import logo from './logo.svg';
import './App.css';
import React, { useState, useEffect } from 'react';
import axios from 'axios';

function UsersPage() {
    const [users, setUsers] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/users')
            .then(response => {
                setUsers(response.data);
            })
            .catch(error => {
                console.error('There was an error!', error);
            });
    }, []);

    return (
        <div>
            <h1>Users</h1>
            {users.map(user => (
                <div key={user.id}>
                    <h2>{user.name ? user.name : 'No name provided'}</h2>
                    <p>Created at: {new Date(user.dateCreated).toLocaleDateString()}</p>
                </div>
            ))}
        </div>
    );
}

function ProductsPage() {
    const [products, setProducts] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/products')
            .then(response => {
                setProducts(response.data);
            })
            .catch(error => {
                console.error('There was an error!', error);
            });
    }, []);

    return (
        <div>
            <h1>Products</h1>
            {products.map(product => (
                <div key={product.id}>
                    <h2>{product.name ? product.name : 'No name provided'}</h2>
                    <p>Price: {product.price}</p>
                </div>
            ))}
        </div>
    );
}

function OrdersPage() {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/orders')
            .then(response => {
                setOrders(response.data);
            })
            .catch(error => {
                console.error('There was an error!', error);
            });
    }, []);

    return (
        <div>
            <h1>Orders</h1>
            {orders.map(order => (
                <div key={order.id}>
                    <h2>Order ID: {order.id}</h2>
                    <p>Date Created: {new Date(order.dateCreated).toLocaleDateString()}</p>
                </div>
            ))}
        </div>
    );
}


function App() {
    return (
      <>
          <UsersPage />
          <ProductsPage />
            <OrdersPage />
        </>
  );
}

export default App;
