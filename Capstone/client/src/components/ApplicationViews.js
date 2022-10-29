import React from "react";
import { Routes, Route, Navigate } from "react-router-dom"
import { BrandForm } from "./brand/BrandForm";
import { BrandList } from "./brand/BrandList";
import { HazardForm } from "./hazards/HazardForm";
import { HazardList } from "./hazards/HazardList";
import Hello from "./Hello";
import { IngredientContainer } from "./ingredient/IngredientContainer";
import { IngredientDelete } from "./ingredient/IngredientDelete";
import { IngredientDetails } from "./ingredient/IngredientDetails";
import { IngredientEdit } from "./ingredient/IngredientEdit";
import { IngredientForm } from "./ingredient/IngredientForm";
import { IngredientList } from "./ingredient/IngredientList";
import { IngredientReviewDelete } from "./ingredientReview/IngredientReviewDelete";
import { IngredientReviewEdit } from "./ingredientReview/ingredientReviewEdit";
import { IngredientReviewForm } from "./ingredientReview/IngredientReviewForm";
import Login from "./Login";
import { ProductContainer } from "./product/ProductContainer";
import { ProductDelete } from "./product/ProductDelete";
import { ProductDetails } from "./product/ProductDetails";
import { ProductEdit } from "./product/ProductEdit";
import { ProductForm } from "./product/ProductForm";
import { ProductList } from "./product/ProductList";
import Register from "./Register";
import { TypeForm } from "./type/TypeForm";
import { TypeList } from "./type/TypeList";
import { UserProductList } from "./userProduct/UserProductList";


export default function ApplicationViews({ isLoggedIn, isApproved, isAdmin }) {
    return (
        <Routes>
            <Route path="/">
                <Route
                    index
                    element={isLoggedIn ? <Hello /> : <Navigate to="/login" />}
                />
                <Route path="product/">
                    <Route index element={isLoggedIn ? <ProductContainer isAdmin={isAdmin} isApproved={isApproved} /> : <Navigate to="/login" />} />
                    <Route path="create" element={isLoggedIn && isAdmin ? <ProductForm isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                    <Route path=":productId" element={isLoggedIn ? <ProductDetails isAdmin={isAdmin} isApproved={isApproved} /> : <Navigate to="/login" />} />
                    <Route path="delete/:productId" element={isLoggedIn && isAdmin ? <ProductDelete isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                    <Route path="edit/:productId" element={isLoggedIn && isAdmin ? <ProductEdit isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                </Route>
                <Route path="ingredient/">
                    <Route index element={isLoggedIn ? <IngredientContainer isAdmin={isAdmin} isApproved={isApproved} /> : <Navigate to="/login" />} />
                    <Route path=":ingredientId" element={isLoggedIn ? <IngredientDetails isAdmin={isAdmin} isApproved={isApproved} /> : <Navigate to="/login" />} />
                    <Route path="create" element={isLoggedIn && isAdmin ? <IngredientForm isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                    <Route path="edit/:ingredientId" element={isLoggedIn && isApproved ? <IngredientEdit isApproved={isApproved} /> : <Navigate to="/login" />} />
                    <Route path="delete/:ingredientId" element={isLoggedIn && isAdmin ? <IngredientDelete isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                </Route>
                <Route path="ingredientReview/">
                    <Route path="create/:ingredientId" element={isLoggedIn && isApproved ? <IngredientReviewForm isApproved={isApproved} /> : <Navigate to="/login" />} />
                    <Route path="delete/:ingredientReviewId" element={isLoggedIn && isApproved ? <IngredientReviewDelete isApproved={isApproved} /> : <Navigate to="/login" />} />
                    <Route path="edit/:ingredientReviewId" element={isLoggedIn && isApproved ? <IngredientReviewEdit isApproved={isApproved} /> : <Navigate to="/login" />} />
                </Route>
                <Route path="type/">
                    <Route index element={isLoggedIn && isAdmin ? <TypeList isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                    <Route path="create" element={isLoggedIn && isAdmin ? <TypeForm isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                </Route>
                <Route path="brand/">
                    <Route index element={isLoggedIn && isAdmin ? <BrandList isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                    <Route path="create" element={isLoggedIn && isAdmin ? <BrandForm isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                </Route>
                <Route path="hazard/">
                    <Route index element={isLoggedIn ? <HazardList isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                    <Route path="create" element={isLoggedIn && isAdmin ? <HazardForm isAdmin={isAdmin} /> : <Navigate to="/login" />} />
                </Route>

                <Route path="myProducts/">
                    <Route index element={isLoggedIn ? <UserProductList isAdmin={isAdmin} isApproved={isApproved} /> : <Navigate to="/login" />} />

                </Route>



                <Route path="login" element={<Login />} />
                <Route path="register" element={<Register />} />
                <Route path="*" element={<p>Whoops, nothing here...</p>} />
            </Route>
        </Routes>
    );
}