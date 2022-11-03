import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Card, CardBody, FormGroup, Input, Label, Form } from "reactstrap"
import { getAllBrands } from "../../modules/brandManager"
import { addProduct } from "../../modules/productManager"
import { getAllTypes } from "../../modules/typeManager"

export const ProductForm = () => {
    const navigate = useNavigate()
    const [types, setTypes] = useState([])
    const [brands, setBrands] = useState([])
    const [product, updateProduct] = useState({
        name: "",
        brandId: 0,
        typeId: 0,
        price: 0,
        imageUrl: ""
    })

    const getTypes = () => {
        getAllTypes().then(t => setTypes(t))
    }

    const getBrands = () => {
        getAllBrands().then(b => setBrands(b))
    }
    useEffect(() => {
        getTypes();
        getBrands();
    }, []);

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        if (product.imageUrl === "") {
            product.imageUrl = null
        }
        const productToSend = {
            name: product.name,
            brandId: parseInt(product.brandId),
            typeId: parseInt(product.typeId),
            price: parseFloat(product.price).toFixed(2),
            imageUrl: product.imageUrl
        }

        return addProduct(productToSend)
            .then(() => {
                navigate("/product")
            })
    }
    return (<>
        <section className="psection">
            <div className="pform">
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
                                    type="select"
                                    onChange={
                                        (evt) => {
                                            const copy = { ...product }
                                            copy.brandId = evt.target.value
                                            updateProduct(copy)
                                        }
                                    } >
                                    <option>choose a brand</option>
                                    {brands.map((b) => (
                                        <option value={b.id} key={b.id}>{b.name}</option>
                                    ))}
                                </Input>
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
                                    <option>choose a type</option>
                                    {types.map((t) => (
                                        <option value={t.id} key={t.id}>{t.name}</option>
                                    ))}
                                </Input>
                            </FormGroup>

                            <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                                className="btn btn-primary">
                                Save
                            </Button>
                            <Button onClick={() => navigate("/product")}>Cancel</Button>
                        </Form>

                    </CardBody>
                </Card>
            </div>
        </section>
    </>)
}