import React from "react";
import { useNavigate } from "react-router-dom";


export const Type = ({ type }) => {
    return (
        <tr>
            <td>{type.name}</td>

        </tr>
    )
}