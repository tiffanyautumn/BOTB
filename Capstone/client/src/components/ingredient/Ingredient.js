import React from "react";
import { Link, NavLink, useNavigate } from "react-router-dom";
import { Button, Card, CardBody, CardImg, Col } from "reactstrap";
import { NavLink as RRNavLink } from "react-router-dom";

export const Ingredient = ({ ingredient }) => {
    const navigate = useNavigate()
    return (
        <tr>
            <td><Button onClick={() => { navigate(`/ingredient/${ingredient.id}`) }}>{ingredient.name}</Button></td>
            <td>{ingredient.function}</td>
            <td>{ingredient.safetyInfo}</td>
            <td>{ingredient?.ingredientReview?.rate?.rating}</td>
        </tr>
    )
}