// ─── Auth ───────────────────────────────────────────────────────────────────
export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
  accountType: string;         // 'Customer' | 'Seller'
  storeName?: string;
  address?: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}

export interface ResetPasswordDTO {
  email: string;
  token: string;
  newPassword: string;
  confirmPassword: string;
}

export interface DecodedToken {
  nameid: string;
  email: string;
  given_name: string;
  family_name: string;
  role: string;
  exp: number;
}

// ─── Customer ───────────────────────────────────────────────────────────────
export interface CustomerDTO {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  address?: string;
  joinedAt?: string;
  isActive: boolean;
}

export interface CustomerWithOrdersDTO {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  address?: string;
  orders: OrderDTO[];
}

export interface UpdateCustomerDTO {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  address?: string;
  joinedAt?: string;
  isActive: boolean;
}

// ─── Seller ─────────────────────────────────────────────────────────────────
export interface SellerDTO {
  id: number;
  storeName: string;
  rating?: number;
  firstName: string;
  lastName: string;
  email: string;
}

export interface SellerWithProductsDTO {
  id: number;
  storeName: string;
  rating?: number;
  firstName: string;
  lastName: string;
  email: string;
  products: ProductDTO[];
}

export interface CreateSellerDTO {
  storeName: string;
  userId: number;
}

export interface UpdateSellerDTO {
  storeName?: string;
  rating?: number;
}

// ─── Product ─────────────────────────────────────────────────────────────────
export interface ProductDTO {
  id: number;
  name: string;
  imageUrl?: string;
  price: number;
  discount: number;
  rating?: number;
  stockQuantity: number;
  description: string;
  categoryId: number;
  sellerId: number;
  reviewCount: number;
}

export interface ProductWithCategoryDTO {
  id: number;
  name: string;
  imageUrl?: string;
  price: number;
  discount: number;
  rating?: number;
  stockQuantity: number;
  description: string;
  categoryName: string;
}

export interface ProductWithSellerDTO {
  id: number;
  name: string;
  imageUrl?: string;
  price: number;
  discount: number;
  rating?: number;
  stockQuantity: number;
  description: string;
  sellerStoreName: string;
}

export interface CreateProductDTO {
  name: string;
  description: string;
  imageUrl?: string;
  price: number;
  discount: number;
  stockQuantity: number;
  categoryId: number;
  sellerId: number;
}

export interface UpdateProductDTO {
  name?: string;
  description?: string;
  imageUrl?: string;
  price?: number;
  discount?: number;
  stockQuantity?: number;
  categoryId?: number;
  sellerId?: number;
}

// ─── Category ────────────────────────────────────────────────────────────────
export interface CategoryDTO {
  id: number;
  name: string;
  description: string;
}

export interface CategoryWithProductsDTO {
  id: number;
  name: string;
  description: string;
  products: ProductDTO[];
}

// ─── Order ───────────────────────────────────────────────────────────────────
export interface OrderDTO {
  id: number;
  customerId: number;
  paymentMethodId: number;
  status: string;
  shippingAddress: string;
  createdAt: string;
  updatedAt?: string;
  deliveredAt?: string;
  totalAmount: number;
  orderItems: OrderItemDTO[];
}

export interface OrderItemDTO {
  productId: number;
  productName: string;
  price: number;
  quantity: number;
  subTotal: number;
}

export interface CreateOrderDTO {
  customerId: number;
  paymentMethodId: number;
  shippingAddress: string;
  orderItems: CreateOrderItemDTO[];
}

export interface CreateOrderItemDTO {
  productId: number;
  quantity: number;
}

// ─── Cart ────────────────────────────────────────────────────────────────────
export interface CartDTO {
  id: number;
  customerId: number;
  cartItems: CartItemDTO[];
  totalPrice: number;
}

export interface CartItemDTO {
  productId: number;
  productName: string;
  productPrice: number;
  quantity: number;
  subTotal: number;
}

// ─── Wishlist ─────────────────────────────────────────────────────────────────
export interface WishlistDTO {
  id: number;
  customerId: number;
  items: WishlistItemDTO[];
}

export interface WishlistItemDTO {
  id: number;
  productId: number;
  productName: string;
  imageUrl?: string;
  price: number;
  rating?: number;
  discount: number;
}

export interface AddToWishlistDTO {
  productId: number;
}

// ─── Payment ──────────────────────────────────────────────────────────────────
export interface PaymentMethodDTO {
  id: number;
  methodName: string;
}

export interface CustomerPaymentDTO {
  id: number;
  customerId: number;
  paymentMethodId: number;
  methodName: string;
}

export interface AddCustomerPaymentDTO {
  customerId: number;
  paymentMethodId: number;
}

// ─── Review ──────────────────────────────────────────────────────────────────
export interface ProductReviewDTO {
  productId: number;
  customerId: number;
  rating: number;
  comment?: string;
}

export interface CreateReviewDTO {
  productId: number;
  customerId: number;
  rating: number;
  comment?: string;
}
