import React, { useState } from "react";
import { deleteBrand } from "../../modules/brandManager";
import { BrandEdit } from "./BrandEdit";

export const Brand = ({ brand, brandDelete, getBrands, brandEdit }) => {

    const [editForm, setEditForm] = useState(false)



    const deleteButton = () => {
        return deleteBrand(brand.id)
            .then(() => {
                getBrands()
            })
    }

    return (


        <section className="brand-details">
            <p className="Name panel-item">
                <span className="title">{brand?.name}
                    {brandDelete ? <button className="btn" onClick={(() => deleteButton())}><i className="fa-solid fa-circle-minus"></i></button> : ""}
                    {brandEdit ? <button className="btn" onClick={(() => setEditForm(!editForm))}><i className="fa-solid fa-eye-dropper"></i></button> : ""}
                </span>
                {editForm ? <BrandEdit Brand={brand} getBrands={getBrands} setEditForm={setEditForm} /> : ""}

            </p>
        </section>
    )
}