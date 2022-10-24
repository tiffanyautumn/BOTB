import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { addProduct, editProduct, getProductById } from "../../modules/productManager"
import { getAllTypes } from "../../modules/typeManager"

export const ProductEdit = () => {
    const navigate = useNavigate()
    const { productId } = useParams()
    const [types, setTypes] = useState([])
    const [product, updateProduct] = useState({
        id: 0,
        name: "",
        brand: "",
        typeId: 0,
        price: 0,
        imageUrl: ""
    })

    const getProduct = () => {
        getProductById(productId).then(p => updateProduct(p))
    }

    useEffect(
        () => {
            getProduct()
        }, []
    )

    const getTypes = () => {
        getAllTypes().then(t => setTypes(t))
    }

    useEffect(() => {
        getTypes();
    }, []);

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        if (product.imageUrl === "") {
            product.imageUrl = null
        }
        const productToSend = {
            id: product.id,
            name: product.name,
            brand: product.brand,
            typeId: parseInt(product.typeId),
            price: parseFloat(product.price).toFixed(2),
            imageUrl: product.imageUrl
        }

        return editProduct(productId, productToSend)
            .then(() => {
                navigate(`/product/${product.id}`)
            })
    }
    return (<>
        <Card>
            <CardBody>

                <Form className="productForm">
                    <FormGroup>
                        <Label for="name">Product</Label>
                        <Input
                            id="product"
                            name="product"
                            type="text"
                            value={product.name}
                            onChange={
                                (evt) => {
                                    const copy = { ...product }
                                    copy.name = evt.target.value
                                    updateProduct(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="brand">Brand</Label>
                        <Input
                            id="brand"
                            name="brand"
                            type="text"
                            value={product.brand}
                            onChange={
                                (evt) => {
                                    const copy = { ...product }
                                    copy.brand = evt.target.value
                                    updateProduct(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="price">Price</Label>
                        <Input
                            id="price"
                            name="price"
                            type="number"
                            min="0.01"
                            step="0.01"
                            value={product.price}
                            onChange={
                                (evt) => {
                                    const copy = { ...product }
                                    copy.price = evt.target.value
                                    updateProduct(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="imageUrl">Image Link</Label>
                        <Input
                            id="imageUrl"
                            name="imageUrl"
                            type="text"
                            value={product.imageUrl}
                            onChange={
                                (evt) => {
                                    const copy = { ...product }
                                    copy.imageUrl = evt.target.value
                                    updateProduct(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="typeSelect">Type</Label>
                        <Input
                            id="typeSelect"
                            name="type"
                            type="select"
                            onChange={
                                (evt) => {
                                    const copy = { ...product }
                                    copy.typeId = evt.target.value
                                    updateProduct(copy)
                                }
                            }>
                            {types.map((t) => (
                                <option value={t.id} key={t.id}>{t.name}</option>
                            ))}
                        </Input>
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => navigate("/product")}>Cancel</Button>
            </CardBody>
        </Card>
    </>)
}