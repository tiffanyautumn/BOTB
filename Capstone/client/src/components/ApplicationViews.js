import React from "react";
import { Routes, Route, Navigate } from "react-router-dom"
import Hello from "./Hello";
import Login from "./Login";
import { ProductDelete } from "./product/ProductDelete";
import { ProductDetails } from "./product/ProductDetails";
import { ProductForm } from "./product/ProductForm";
import { ProductList } from "./product/ProductList";
import Register from "./Register";


export default function ApplicationViews({ isLoggedIn }) {
    return (
        <Routes>
            <Route path="/">
                <Route
                    index
                    element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
                />
                <Route path="product/create" element={<ProductForm />} />
                <Route path="product" element={<ProductList />} />
                <Route path="product/:productId" element={<ProductDetails />} />
                <Route path="product/delete/:productId" element={<ProductDelete />} />
                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
                <Route path="*" element={<p>Whoops, nothing here...</p>} />
            </Route>
        </Routes>
    );
}